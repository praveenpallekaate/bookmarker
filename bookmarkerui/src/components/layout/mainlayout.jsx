import React from 'react';
import {Layout} from 'antd';
import AppHeader from './header.jsx';
import AppContent from './content.jsx';
import './mainlayout.less';

function MainLayout() {
  const {Header, Content, Footer} = Layout;

  return (
    <Layout className="layout">
      <Header>
        <AppHeader/>
      </Header>
      <Content className="content">
        <AppContent/>
      </Content>
      <Footer>© SNC-Lavalin {new Date().getFullYear()}</Footer>
    </Layout>  
  );
}

export default MainLayout;