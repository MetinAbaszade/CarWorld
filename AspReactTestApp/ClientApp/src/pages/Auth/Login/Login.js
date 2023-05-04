import React, { useRef, useEffect } from "react";
import { useLocation, useNavigate } from "react-router-dom";
import { AuthService, CookieService } from '../../../Services';
import './Login.css';

export default function Login() {
  const nameInput = useRef();
  const passwordInput = useRef();
  const errorLabel = useRef();
  const spinner = useRef();
  const alert = useRef();
  const navigate = useNavigate();


  useEffect(() => {
    document.body.style.backgroundImage = "url('/fluid.svg')";
    document.body.style.backgroundPosition = 'center';
    document.body.style.backgroundRepeat = 'no-repeat';

    // Clean up function to reset the background color when the component is unmounted
    return () => {
      document.body.style.backgroundImage = '';
      document.body.style.backgroundColor = 'white';
    };
  }, []);

  const SignInHandle = async (e) => {
    e.preventDefault();
    
    let userName = nameInput.current.value;
    let password = passwordInput.current.value;

    let authResponse = await AuthService.Login(userName, password);

    if (authResponse.isSuccessfull) {
      CookieService.setCookie("refreshtoken", authResponse.refreshToken);
      CookieService.setCookie("accesstoken", authResponse.token);

      let refreshtoken = CookieService.getCookie("refreshtoken");
      let accesstoken = CookieService.getCookie("accesstoken");
      console.log("accesstoken: " + accesstoken);
      console.log("refreshtoken: " + refreshtoken);

     
      navigate("/Profile");
    }
    else {
      console.log(errorLabel);
      errorLabel.current.innerHTML = "Email or Password is wrong";
    }
  };

  //#region Spinner
  
  const updateUi = async (value) => {
    spinner.current.classList.remove("visible");

    const usernameExists = await AuthService.CheckUserExists(value);

    if (usernameExists) {
      alert.current.classList.add("visible");
    } else {
      alert.current.classList.remove("visible");
    }
  };

  const debounce = (callback, time) => {
    let interval;
    return (...args) => {
      clearTimeout(interval);
      interval = setTimeout(() => {
        callback.apply(null, args);
      }, time);
    };
  };

  const handleUsernameChange = debounce((input) => {
    const { value } = input.target;
    updateUi(value);
  }, 500);

  const handleStartTyping = () => {
    spinner.current.classList.add("visible");
  };

  //#endregion


  return (
    <>
      <div className="login-container">
        <div className="login-card">
          <img
            src="https://pub-static.fotor.com/assets/projects/pages/5ff61721271e45d2b9bbc6dbbd4b14c7/300w/purple-cute-school-girl-78a8ba2c107c4ce1bb7e5a3de0ed9528.jpg"
          />
          <form className="login-form" onSubmit={SignInHandle}>
            <div className="username">
              <input
                spellCheck="false"
                className="control"
                ref={nameInput}
                type="text"
                placeholder="Username"
                onKeyUp={handleUsernameChange }
                onKeyDown={handleStartTyping}
              />
              <div ref={spinner} className="spinner"></div>
            </div>
            <div ref={alert} className="alert">Username already exists</div>
            <input
              spellCheck="false"
              className="control"
              ref={passwordInput}
              type="password"
              placeholder="Password"
            />
            <button className="control" type="button" onClick={() => { navigate('/auth/register') }}>JOIN NOW</button>
            <button className="control" type="button">Sign In</button>
          </form>
        </div>
      </div>
    </>
  );
}
