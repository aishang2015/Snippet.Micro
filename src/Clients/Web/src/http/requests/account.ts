import { CommonResult } from "../common-result";
import { Axios } from "../request";

export function login(model: LoginModel) {

    return Axios.instance.request<LoginResult>({
        url: 'identity/connect/token',
        method: 'post',
        data: {
            grant_type: 'password',
            client_id: 'snippet.micro.web',
            username: model.userName,
            password: model.password
        },
        transformRequest: [
            function (data) {
                let ret = ''
                for (let it in data) {
                    ret += encodeURIComponent(it) + '=' + encodeURIComponent(data[it]) + '&'
                }
                ret = ret.substring(0, ret.lastIndexOf('&'));
                return ret
            }
        ],
        headers: {
            'Content-Type': 'application/x-www-form-urlencoded'
        }
    });
}

export function getUserInfo() {
    return Axios.instance.post<CommonResult<UserInfoResult>>('rbac/api/account/getCurrentUserInfo', null);
}

export function handleThirdPartyCode(code: string, source: string) {
    return Axios.instance.post<CommonResult<ThirdPartyLoginResult>>(`/api/account/ThirdPartyLogin`, {
        code: code,
        source: source
    });
}

export function bindingThirdPartyAccount(model: BindingModel) {
    return Axios.instance.post<CommonResult<LoginResult>>('rbac/api/account/BindingThirdPartyAccount', model);
}

export function refresh(userName: string, jwtToken: string, refreshToken: string) {
    return Axios.instance.post<CommonResult<LoginResult>>('rbac/api/account/refresh', {
        userName: userName,
        jwtToken: jwtToken,
        refreshToken: refreshToken
    });
}

export interface LoginModel {
    userName: string;
    password: string;
}

export interface LoginResult {
    access_token: string;
    expires_in: number;
}

export interface UserInfoResult {
    userName: string;
    identities: string[];
}

export interface ThirdPartyLoginResult {
    accessToken: string;
    userName: string;
    thirdPartyType: string;
    thirdPartyUserName: string;
    thirdPartyInfoCacheKey: string;
}

export interface BindingModel {
    userName: string;
    password: string;
    thirdPartyType: string;
    thirdPartyInfoCacheKey: string;
}