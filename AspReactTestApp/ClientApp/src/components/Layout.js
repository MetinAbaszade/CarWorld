import React, { Component } from 'react';
import { Container } from 'reactstrap';
import { NavMenu } from './NavMenu';

export class Layout extends Component {
    static displayName = Layout.name;

    render() {
        return (
            <div className='w-100 h-100'>
                <NavMenu />
                {this.props.children}
            </div>
        );
    }
}
