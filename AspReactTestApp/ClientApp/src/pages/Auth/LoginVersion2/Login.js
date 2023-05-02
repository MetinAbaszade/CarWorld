import React, { useRef } from "react";
import { useLocation, useNavigate } from "react-router-dom";
import { AuthService, CookieService } from '../../../Services';
import './Login.css';

export default function Login() {

    const nameInput = useRef();
    const passwordInput = useRef();
    const errorLabel = useRef();

    const navigate = useNavigate();

    const location = useLocation();

    const LoginHandle = async (e) => {
        e.preventDefault();
        let userName = nameInput.current.value;
        let password = passwordInput.current.value;

        let authResponse = await AuthService.Login(userName, password);
        console.log(authResponse);

        if (authResponse.isSuccessfull) {
            CookieService.setCookie("refreshtoken", authResponse.refreshToken);
            CookieService.setCookie("accesstoken", authResponse.token);
            let refreshtoken = CookieService.getCookie("refreshtoken");
            let accesstoken = CookieService.getCookie("accesstoken");
            console.log("accesstoken: " + accesstoken);
            console.log("refreshtoken: " + refreshtoken);
        }
        else { 
            console.log(errorLabel);
            errorLabel.current.innerHTML = "Email or Password is wrong";
        }
    };

    return (
        <>
            <div className="login-box">
                <h2>Login</h2>
                <form onSubmit={LoginHandle}>
                    <div className="user-box">
                        <input ref={nameInput} type="text" name="" required="" placeholder=" " />
                        <label>Username</label>
                    </div>
                    <div className="user-box">
                        <input ref={passwordInput} type="password" name="" required="" placeholder=" " />
                        <label>Password</label>
                    </div>
                    <div>
                        <label ref={errorLabel} className="text-danger"></label>
                        </div>
                    <button href="#" type="submit">
                        <span></span>
                        <span></span>
                        <span></span>
                        <span></span>
                        Submit
                    </button>
                </form>
            </div>
        </>
    );
}
