import React, {Component} from 'react';
import {Space, Card, Skeleton, Row, Col, Button} from 'antd';
import {PlusOutlined, SortDescendingOutlined} from '@ant-design/icons';
import Search from 'antd/lib/input/Search';
import Tile from '../common/tiles.jsx';
import PopupModal from '../common/popupmodal.jsx';
import TileManage from '../layout/tilemanage.jsx';
import {TILETYPE_ENUM} from '../../enums/index';
import {TILEDETAILS_CONST, COMMON_CONST} from '../../constants/index';
import CatalogueAPI from '../../api/catalogue';
import Helper from '../../utils/helper';
import './content.less';

class AppContent extends Component {
  constructor(props){
    super(props);

    this.state = {
      data: {},
      tileModal: {
        title: '',
        details: {},
        type: TILETYPE_ENUM.add,
        show: false,
        stopCallBack: false
      },
      searchText: '',
      isFiltered: false,
      isSorted: false
    };
  }

  componentDidMount = () => {
    this.loadCatalogues();
  }

  handleManageTile = (val) => {
    if(val){
      let {data, tileModal} = this.state;

      tileModal.show = !tileModal.show;
      tileModal.type = val.type;
      tileModal.stopCallBack = false;

      if(val.type === TILETYPE_ENUM.edit){
        const tilesDetails = (data || {}).catalogueList || [];
        const tileDetails = (tilesDetails.filter(i => i.id === val.id)[0] || {});

        tileModal.title = TILEDETAILS_CONST.editTitle;
        tileModal.details = tileDetails;
      } else {
        tileModal.title = TILEDETAILS_CONST.addTitle;
        tileModal.details = {};
      }
  
      this.setState({tileModal: tileModal});  
    }
  }

  handleModalSubmit = (val) => {
    if(val){
      let {tileModal} = this.state;

      tileModal.show = !tileModal.show;
      tileModal.details = {};

      if(val.type === TILETYPE_ENUM.add){
        const catalogue = this.initializeTile(val);

        CatalogueAPI
          .addCatalogue(catalogue)
          .then((response) => {
            if(Helper.isSuccess(response)){
              this.loadCatalogues();
              this.setState({tileModal: tileModal});  
            }
          })
          .catch((error) => {
          });
      } else if(val.type === TILETYPE_ENUM.edit){
        const catalogue = {
          type: COMMON_CONST.catalogueType,
          name: val.name,
          details: val.details,
          path: val.path,
          tags: val.tags,
          category: val.category,
          tileImageUrl: val.tileImageUrl,
          id: val.id
        };

        CatalogueAPI
          .editCatalogue(catalogue)
          .then((response) => {
            if(Helper.isSuccess(response)){
              this.loadCatalogues();
              this.setState({tileModal: tileModal});  
            }
          })
          .catch((error) => {
          });
      }
    }
  }

  handleModalCancel = () => {
    let {tileModal} = this.state;

    tileModal.show = !tileModal.show;

    this.setState({tileModal: tileModal});  
  }

  handleRemoveTile = (val) => {
    if(val){
      CatalogueAPI
        .removeCatalogue(COMMON_CONST.catalogueType, val)
        .then((response) => {
          if(Helper.isSuccess(response)){
            this.loadCatalogues();
          }
        })
        .catch((error) => {
        });
    }
  }

  handleFormRefresh = () => {
    let {tileModal} = this.state;

    tileModal.show = !tileModal.show;
    tileModal.stopCallBack = true;

    this.setState({tileModal: tileModal}, () => {
      tileModal.show = !tileModal.show;

      this.setState({tileModal: tileModal});
    });  
  }

  searchTile = (val) => {
    if(val){
      this.setState({
        searchText: val,
        isFiltered: true
      });
    } else {
      this.setState({
        searchText: '',
        isFiltered: false
      });
    }
  }

  sortToggle = () => {
    let {isSorted} = this.state;

    this.setState({isSorted: !isSorted});
  }

  loadCatalogues = () => {
    let {data} = this.state;
    
    try {
      CatalogueAPI
        .getCatalogues(COMMON_CONST.catalogueType)
        .then((response) => {
          if(Helper.isSuccess(response)){
            if(response.data){
              data = response.data;

              this.setState({
                data: data
              });
            }
          }
        })
        .catch((error) => {
          
        });
    } catch (error) {
      
    }
  }

  initializeTile = (val) => {    
    return {
      type: COMMON_CONST.catalogueType,
      catalogueList: [{
        name: val.name,
        details: val.details,
        path: val.path,
        tags: val.tags,
        category: val.category,
        tileImageUrl: val.tileImageUrl
      }]
    };
  }

  render() {
    const {
      data, 
      tileModal,
      searchText,
      isFiltered,
      isSorted
    } = this.state;
    const tilesDetails = (data || {}).catalogueList || [];
    const colFlex = "0 1 300px";    
    let processedTiles = [];
    let sortedTiles = [];
    
    if(tilesDetails.length > 0){
      sortedTiles = isSorted ? [...tilesDetails].reverse() : tilesDetails;
      processedTiles = isFiltered 
        ? sortedTiles.filter(i => i.name.toLowerCase().includes(searchText.trim().toLowerCase())) 
        : sortedTiles;  
    }
    return (
      <div className="site-layout-content">
        <Space 
          direction="vertical" 
          size="middle"
        >
          <Row gutter={[16,16]} justify="end">
            <Col flex="0 1 250px">
              <Search
                placeholder="Search"
                className="search"
                onSearch={value => this.searchTile(value)}
              />
            </Col>
            <Col flex="0 1 50px">
              <Button
                block
                icon={<SortDescendingOutlined />}
                type="link"
                onClick={this.sortToggle}
              ></Button>
            </Col>
          </Row>
          <div>
          {
            processedTiles.length > 0
            ? (
                <Row justify="space-between" gutter={[16,16]}>
                {
                  processedTiles.map((item, index) => {
                    if(item.isActive){
                      return (
                        <Col flex={colFlex}>
                          <Tile
                            key={'tile' + index}
                            data={item}
                            handleEdit={this.handleManageTile}
                            handleRemove={this.handleRemoveTile}
                          />
                        </Col>
                      )  
                    }
                  })
                }                
                </Row>
              )
            : (
                <Card 
                  hoverable 
                  className="load-card"
                >
                  <Skeleton active />
                </Card>
              )
          }
            <Card 
              hoverable
              className="load-card"
            >
              <Button 
                block
                icon={<PlusOutlined className="large-icon" />}
                type="link"
                className="height-150px"
                onClick={() => this.handleManageTile({type:TILETYPE_ENUM.add})}
              ></Button>
            </Card>
          </div>        
        </Space>
        <PopupModal 
          show={tileModal.show} 
          title={tileModal.title}
          handleSubmit={this.handleModalSubmit}
          handleCancel={this.handleModalCancel}
          footer={null}
        >
          <TileManage 
            type={tileModal.type} 
            data={tileModal.details}
            stopCallBack={tileModal.stopCallBack}
            handleSubmit={this.handleModalSubmit} 
            callBack={this.handleFormRefresh}
          />
        </PopupModal>
      </div>
    )
  }
}

export default AppContent;