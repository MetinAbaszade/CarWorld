import React, { Component } from 'react';
import AppRoutes from './components/AppRoutes';
import { Layout } from './components/Layout';
import "./App.css";
import Login from './pages/Auth/Login/Login';

export default class App extends Component {
    static displayName = App.name;

    render() {
        return (
            // <Layout>
            //     <AppRoutes />
            // </Layout>

            <Login />
        );
    }
}
