import BasicLayout from './pages/layout/layout'
import {
  BrowserRouter as Router,
  Redirect,
  Route,
  Switch,
} from "react-router-dom";
import './App.less';
import Login from './pages/login/login';
import React, { Suspense } from 'react';
import Callback from './pages/callback/callback';
import { Bind } from './pages/bind/bind';
import { StorageService } from './common/storage';
import { react } from '@babel/types';

interface app {
}

class App extends React.Component<any, app> {

  constructor(props: any) {
    super(props);
    this.state = {
    }
  }

  render() {

    // 客户端判断token是否过期，过期则清理登录信息
    let isOutOfDate = false;
    let expireString = StorageService.getExpire();
    if (expireString) {
      let expireTime = new Date(expireString);
      if (expireTime < new Date()) {
        isOutOfDate = true;
      }
    } else {
      isOutOfDate = true;
    }

    if (isOutOfDate) {
      StorageService.clearLoginStore();
    }

    const HomePage = React.lazy(() => import('./pages/home/home'));
    const AboutPage = React.lazy(() => import('./pages/about/about'));
    const TablePage = React.lazy(() => import('./pages/table/table'));
    const FlowPage = React.lazy(() => import('./pages/flow/flow'));
    const ChatPage = React.lazy(() => import('./pages/chat/chat'));

    const UserPage = React.lazy(() => import('./pages/system/user/user'));
    const RolePage = React.lazy(() => import('./pages/system/role/role'));
    const PagePage = React.lazy(() => import('./pages/system/page/page'));
    const OrgPage = React.lazy(() => import('./pages/system/org/org'));
    const StatePage = React.lazy(() => import('./pages/system/state/state'));

    return (
      <Router>
        <Switch>
          <Route path="/" render={
            ({ location }) => StorageService.getAccessToken() && StorageService.getUserName() ? (
              <BasicLayout>
                <Switch>
                  <Route exact={true} path="/home"><Suspense fallback="加载中..."><HomePage /></Suspense></Route>
                  <Route exact={true} path="/table"><Suspense fallback="加载中..."><TablePage /></Suspense></Route>
                  <Route exact={true} path="/flow"><Suspense fallback="加载中..."><FlowPage /></Suspense></Route>
                  <Route exact={true} path="/chat"><Suspense fallback="加载中..."><ChatPage /></Suspense></Route>
                  <Route exact={true} path="/about"><Suspense fallback="加载中..."><AboutPage /></Suspense></Route>

                  <Route exact={true} path="/user"><Suspense fallback="加载中..."><UserPage /></Suspense></Route>
                  <Route exact={true} path="/role"><Suspense fallback="加载中..."><RolePage /></Suspense></Route>
                  <Route exact={true} path="/page"><Suspense fallback="加载中..."><PagePage /></Suspense></Route>
                  <Route exact={true} path="/org"><Suspense fallback="加载中..."><OrgPage /></Suspense></Route>
                  <Route exact={true} path="/state"><Suspense fallback="加载中..."><StatePage /></Suspense></Route>
                  
                  <Route path="*">
                    <Redirect to="/home"></Redirect>
                  </Route>
                </Switch>
              </BasicLayout>
            ) : (
              <Switch>
                <Route exact={true} path="/login" component={Login}></Route>
                <Route exact={true} path="/callback" component={Callback}></Route>
                <Route exact={true} path="/binding" component={Bind}></Route>
                <Route path="*">
                  <Redirect to="/login"></Redirect>
                </Route>
              </Switch>
            )
          } />
        </Switch>
      </Router>
    );
  }
}

export default App;