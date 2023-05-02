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
  const bars = useRef();
  const strengthDiv = useRef();


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


  //#region strengthbar

  const strength = {
    1: "weak",
    2: "medium",
    3: "strong",
  };

  const getIndicator = (password, strengthValue) => {
    for (let index = 0; index < password.length; index++) {
      let char = password.charCodeAt(index);
      if (!strengthValue.upper && char >= 65 && char <= 90) {
        strengthValue.upper = true;
      } else if (!strengthValue.numbers && char >= 48 && char <= 57) {
        strengthValue.numbers = true;
      } else if (!strengthValue.lower && char >= 97 && char <= 122) {
        strengthValue.lower = true;
      }
    }

    let strengthIndicator = 0;

    for (let metric in strengthValue) {
      if (strengthValue[metric] === true) {
        strengthIndicator++;
      }
    }

    return strength[strengthIndicator] ?? "";
  };

  const getStrength = (password) => {
    let strengthValue = {
      upper: false,
      numbers: false,
      lower: false,
    };

    return getIndicator(password, strengthValue);
  };

  const handlePasswordChange = () => {
    let password = passwordInput.current.value;

    console.log(passwordInput.current.value);

    const strengthText = getStrength(password);

    bars.current.classList = "";

    if (strengthText) {
      strengthDiv.current.innerHTML = `${strengthText} Password`;
      bars.current.classList.add(strengthText);
    } else {
      strengthDiv.current.innerHTML = "";
    }
  };

  //#endregion


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

  const handleChange = debounce((input) => {
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
          <form className="login-form" onSubmit={LoginHandle}>
            <div className="username">
              <input
                spellCheck="false"
                className="control"
                ref={nameInput}
                type="text"
                placeholder="Username"
                onKeyUp={handleChange}
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
              onKeyUp={handlePasswordChange}
            />
            <div ref={bars} id="bars">
              <div></div>
            </div>
            <div className="strength" ref={strengthDiv} id="strength"></div>
            <button className="control" type="button">JOIN NOW</button>
            <button className="control" type="button">LOGIN</button>
          </form>
        </div>
      </div>
    </>
  );
}
