import React, { useRef, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { AuthService, CookieService } from '../../../Services';
import '../Auth.css';
import LoginCard from "./LoginCard/LoginCard";
import Carousel from "../Carousel/CarouselCompenent";

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
    <div className="primary-container">
      <div className="secondary-container">
        <div className="left">
          <Carousel />
        </div>
        <div className="right login-card">
          <LoginCard />
        </div>
      </div>
    </div>
  );
}
