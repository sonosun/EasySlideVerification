import request from "@/utils/request.js"

export function getVerification() {
    return request({
        url: '/home/getVerification',
        method: 'get'
    })
}

export function verify(param) {
    return request({
        url: '/home/verify',
        method: 'post',
        data: param
    })
}