import React from 'react';
import { Card, Tooltip } from 'antd';
import { EditOutlined, DeleteOutlined } from '@ant-design/icons';
import {TILETYPE_ENUM} from '../../enums/index';
import Helper from '../../utils/helper';
import './tiles.less';

function Tile(props) {
  const {Meta} = Card;

  return (
    <div>
      <Card
        hoverable
        className="load-card"
        actions={[            
          <EditOutlined onClick={() => props.handleEdit({type:TILETYPE_ENUM.edit, id: props.data.id})}/>,
          <DeleteOutlined onClick={() => props.handleRemove(props.data.id)}/>
        ]}          
        cover={
          <img 
            alt="icon" 
            src={props.data.tileImageUrl}
            onClick={() => {window.open(props.data.path, '_blank')}}
          />
        }
      >
        <a
          href={props.data.path}
          target="_blank"
        >
        <Tooltip 
          title={props.data.name + ' - ' + props.data.details}
          mouseEnterDelay={1}
        >
          <Meta
            title={Helper.getPascalCaseString(Helper.wrapLongTextWithDots(props.data.name, 27))}
            description={Helper.getPascalCaseString(Helper.wrapLongTextWithDots(props.data.details, 160))}
          />
        </Tooltip>          

        </a>
      </Card>
    </div>
  );
}

export default Tile;