import React from 'react';
import { Modal } from 'antd';
import './popupmodal.less';

function PopupModal(props) {
  return (
    <Modal
      title={props.title}
      visible={props.show}
      onOk={props.handleSubmit}
      onCancel={props.handleCancel}
      footer={props.footer}
    >
    {
      props.children
    }
    </Modal>
  );
}

export default PopupModal;