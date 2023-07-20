import React, { Component } from 'react';

export class Layout extends Component {
    static displayName = Layout.name;

    render() {
        return (
            <div className='w-100 h-100'>
                {this.props.children}
            </div>
        );
    }
}
