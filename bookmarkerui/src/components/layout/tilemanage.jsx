import React, {useEffect, useState} from 'react';
import {TILETYPE_ENUM} from '../../enums/index';
import { Form, Input, Button } from 'antd';
import './tilemanage.less';

function TileManage(props) {
  const [form] = Form.useForm();
  let tileDetails = setDefaults();
  const [initialFormValues, setInitialFormValues] = useState(tileDetails);
  
  useEffect(() => {
    if(!props.stopCallBack){      
      tileDetails = setDefaults();
      setInitialFormValues(tileDetails);
      form.resetFields();
      props.callBack();
    }
  }, [props])

  function setDefaults(){
    return {
      id: ((props.data || {}).id || ''),
      name: ((props.data || {}).name || ''),
      details: ((props.data || {}).details || ''),
      path: ((props.data || {}).path || ''),
      tags: ((props.data || {}).tags || ''),
      category: ((props.data || {}).category || ''),
      tileImageUrl: ((props.data || {}).tileImageUrl || ''),
      type: (props.type || '')
    };
  }

  function onFinish(values) {
    props.handleSubmit({
      id: tileDetails.id,
      name: values.name,
      details: values.details,
      path: values.path,
      tags: values.tags,
      category: values.category,
      tileImageUrl: values.tileImageUrl,
      type: tileDetails.type
    });
  }

  return (
    <div>
      {form.resetFields()}
      <Form
        name="addTile"
        layout="horizontal"
        form={form}
        onFinish={onFinish}
        initialValues={initialFormValues}
      >
        <Form.Item 
          name="name"
          rules={[{ required: true, message: 'Please input your site name!' }]}
        >
          <Input
            placeholder="Site Name"
          />
        </Form.Item>
        <Form.Item 
          name="details"
          rules={[{ required: true, message: 'Please input your site details!' }]}
        >
          <Input
            placeholder="Site Description"
          />
        </Form.Item>
        <Form.Item 
          name="path"
          rules={[{ required: true, message: 'Please input your site url!' }]}
        >
          <Input
            placeholder="Site URL"
          />
        </Form.Item>
        <Form.Item 
          name="tags"
          rules={[{ required: true, message: 'Please input your site tags!' }]}
        >
          <Input
            placeholder="tags"
          />
        </Form.Item>
        <Form.Item 
          name="category"
        >
          <Input
            placeholder="Category"
          />
        </Form.Item>
        <Form.Item 
          name="tileImageUrl"
        >
          <Input
            placeholder="Tile Image URL"
          />
        </Form.Item>
        <Form.Item>
          <Button
            type="primary"
            htmlType="submit"
          >
            {
              tileDetails.type === TILETYPE_ENUM.add
              ? 'Submit' : 'Update'
            }
          </Button>
        </Form.Item>
      </Form>
    </div>
  );
}

export default TileManage;