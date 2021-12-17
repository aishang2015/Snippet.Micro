import './home.less';

import React from "react";
import { getUserInfo } from '../../http/requests/account';

interface home {
    id?: number;
    userName?: string;
    email?: string;
    phoneNumber?: string;
}

export default class Home extends React.Component<any, home>{

    constructor(props: any) {
        super(props);
        this.state = {};
    }

    async componentDidMount() {
        try {
            
        } catch (err) {
            return;
        }
    }

    render() {
        return (
            <div>
            </div>
        );
    }


}