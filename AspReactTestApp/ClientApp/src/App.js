import React, { Component } from 'react';
import AppRoutes from './components/AppRoutes';
import { Layout } from './components/Layout';
import store from './redux-toolkit';
import { Provider } from 'react-redux';
import "./App.css";

export default class App extends Component {
    static displayName = App.name;

    render() {
        return (
            <Provider store={store}>
                <Layout>
                    <AppRoutes />
                </Layout>
            </Provider>
        );
    }
}

