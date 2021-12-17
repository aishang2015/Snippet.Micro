import { faClipboardCheck, faCog, faColumns, faHome, faInfo, faTasks, faThumbtack, faUniversalAccess, faUser, faUsers, faUserTag } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';

export class Constants {

    static RouteInfo = [
        { path: '/home', name: '主页', identify: 'home', icon: <FontAwesomeIcon icon={faHome} fixedWidth /> },
        {
            path: '', name: 'RBAC管理', identify: 'rbac', icon: <FontAwesomeIcon icon={faUniversalAccess} fixedWidth />, children: [
                { path: '/user', name: '用户信息', identify: 'user', icon: <FontAwesomeIcon icon={faUser} fixedWidth /> },
                { path: '/role', name: '角色信息', identify: 'role', icon: <FontAwesomeIcon icon={faUserTag} fixedWidth /> },
                { path: '/org', name: '组织信息', identify: 'org', icon: <FontAwesomeIcon icon={faUsers} fixedWidth /> },
                { path: '/page', name: '页面权限', identify: 'permission', icon: <FontAwesomeIcon icon={faColumns} fixedWidth /> },
            ]
        }
    ];

    static FlatRouteInfo = [
        ...Constants.RouteInfo,
        ...Constants.RouteInfo[1].children!
    ];

    static ElementTypeArray = [
        { key: 1, value: '菜单' },
        { key: 2, value: '按钮/链接' }
    ]

    static ElementTypeDic: { [key: number]: string } = {
        1: '菜单',
        2: '按钮/链接'
    }
}