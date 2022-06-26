import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { FetchData } from './components/FetchData';
import { Counter } from './components/Counter';
import AuthorizeRoute from './components/api-authorization/AuthorizeRoute';
import ApiAuthorizationRoutes from './components/api-authorization/ApiAuthorizationRoutes';
import { ApplicationPaths } from './components/api-authorization/ApiAuthorizationConstants';

import './custom.css'
import IndexComponent from './views/employees/IndexComponent';
import EditComponent from './views/employees/EditComponent';
import CreateComponent from './views/employees/CreateComponent';
import CalculateComponent from './views/employees/CalculateComponent';

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
        <Layout>
          <Route exact path='/' component={IndexComponent} />
          <Route path='/counter' component={Counter} />
          <AuthorizeRoute path='/fetch-data' component={FetchData} />
          <AuthorizeRoute path='/employees/index' component={IndexComponent} />
          <AuthorizeRoute path='/employees/create' component={CreateComponent} />
          <AuthorizeRoute path='/employees/:id/edit' component={EditComponent} />
          <AuthorizeRoute path='/employees/:id/calculate' component={CalculateComponent} />
          <Route path={ApplicationPaths.ApiAuthorizationPrefix} component={ApiAuthorizationRoutes} />
        </Layout>
    );
  }
}
