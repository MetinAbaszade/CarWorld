import Cookies from 'js-cookie';

export function setCookie(cName, cValue, expDays) {
    let date = new Date();
    date.setTime(date.getTime() + (expDays * 24 * 60 * 60 * 1000));
    const expires = "expires=" + date.toUTCString();
    document.cookie = cName + "=" + cValue + "; " + expires + "; path=/";
}

export function getCookie(cName) {
    const name = cName + "=";
    const cookieDecoded = decodeURIComponent(document.cookie); //to be careful
    const cookieArr = cookieDecoded.split('; ');
    let result = null;
    cookieArr.forEach(val => {
        if (val.indexOf(name) === 0) result = val.substring(name.length);
    })
    return result;
}