import React, { Component } from 'react';
import AppRoutes from './components/AppRoutes';
import { Layout } from './components/Layout';
import "./App.css";

export default class App extends Component {
    static displayName = App.name;

    render() {
        return (
            <Layout>
                <AppRoutes />
            </Layout>
        );
    }
}
