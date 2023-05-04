import React, { useState, useRef, useEffect, useCallback } from "react";
import { useNavigate } from "react-router-dom";
import { AuthService, CookieService } from '../../../Services';
import { useFormik } from 'formik';
import * as Yup from 'yup';
import { Swiper, SwiperSlide } from 'swiper/react';
import SwiperCore, { Navigation } from 'swiper/core';
import 'swiper/css';
import 'swiper/css/navigation';
import './Register.css';

SwiperCore.use([Navigation]);


export default function Register() {
  const errorLabel = useRef();
  const spinner = useRef();
  const alert = useRef();
  const bars = useRef();
  const navigate = useNavigate();

  //#region Changing page background in Login:
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
  //#endregion


  //#region Schemas to validate each page:
  const FirstPageSchema = Yup.object().shape({
    name: Yup.string().required('Name is required'),
    surname: Yup.string().required('Surname is required'),
    userName: Yup.string().required('Username is required'),
    password: Yup.string()
      .min(6, 'Password must be at least 6 characters')
      .required('Password is required'),
    retypePassword: Yup.string()
      .oneOf([Yup.ref('password'), null], 'Passwords must match')
      .required('Retype password is required'),
  });

  const SecondPageSchema = Yup.object().shape({
    email: Yup.string().email("Invalid email").required("Email is required"),
  });
  //#endregion
 

  const formik = useFormik({
    initialValues: {
      email: '',
      name: '',
      surname: '',
      userName: '',
      password: '',
      retypePassword: '',
    },
    onSubmit: async (values) => {

      let userName = values.name
      let password = values.password;

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
    },
  });

  const validateForm = async (schema, valuesToValidate) => {
    try {
      await schema.validate(valuesToValidate, { abortEarly: false });
      formik.setErrors({});
      return true;
    } catch (err) {
      let errorMessages = err.inner.reduce((errors, error) => {
        return { ...errors, [error.path]: error.message };
      }, {});

      formik.setErrors(errorMessages);
      return false;
    }
  }


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

  const handlePasswordChange = (event) => {
    let password = event.target.value;

    const strengthText = getStrength(password);

    bars.current.classList = "";

    if (strengthText) {
      bars.current.classList.add(strengthText);
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

  const handleUsernameChange = useCallback(
    debounce(async (input) => {
      const { value } = input.target;
      formik.values.userName = value;

      await updateUi(value);
    }, 500),
    [updateUi, formik]
  );

  const handleStartTyping = () => {
    spinner.current.classList.add("visible");
  };

  //#endregion

  //#region Swiper

  const swiperRef = useRef();

  const handleNextSlide = async () => {
    const activeIndex = swiperRef.current.activeIndex;
    let schema, valuesToValidate, valid;

    // For each page, we have different schemas and values to validate
    if (activeIndex === 0) {
      valuesToValidate = {
        name: formik.values.name,
        surname: formik.values.surname,
        userName: formik.values.userName,
        password: formik.values.password,
        retypePassword: formik.values.retypePassword,
      };
      schema = FirstPageSchema;
    }

    else if (activeIndex === 1) {
      valuesToValidate = {
        email: formik.values.email
      };
      schema = SecondPageSchema;
    };

    valid = await validateForm(schema, valuesToValidate);

    if (valid) {
      swiperRef.current.slideNext();
      return;
    }
  };

  const handlePrevSlide = () => {
    if (swiperRef.current) {
      swiperRef.current.slidePrev();
    }
  };
  //#endregion

  return (
    <div className="login-container">
      <div className="login-card">
        <div className="text-center">
          <img
            src="/UserLogo.png"
          />
        </div>
        <form className="login-form" onSubmit={formik.handleSubmit}>
          <Swiper
            onSwiper={(swiper) => (swiperRef.current = swiper)}
            ref={swiperRef}
            allowTouchMove={false}
            spaceBetween={50}
            slidesPerView={1}
          >
            <SwiperSlide>
              <div>
                <input
                  className="control mb-0"
                  type="name"
                  name="name"
                  placeholder="Name"
                  onChange={formik.handleChange}
                  onBlur={formik.handleBlur}
                  value={formik.values.name}
                />

                {formik.errors.name ? (
                  <label className="text-danger ms-1">{formik.errors.name}</label>
                ) : <label className="text-danger ms-1"></label>}
              </div>

              <div>
                <input
                  type="surname"
                  name="surname"
                  className="control mb-0"
                  placeholder="Surname"
                  onChange={formik.handleChange}
                  onBlur={formik.handleBlur}
                  value={formik.values.surname}
                />
                {formik.errors.surname ? (
                  <label className="text-danger ms-1">{formik.errors.surname}</label>
                ) : <label className="text-danger ms-1"></label>}
              </div>

              <div className="username">
                <input
                  type="userName"
                  name="userName"
                  spellCheck="false"
                  className="control mb-0"
                  placeholder="Username"
                  onChange={formik.handleChange}
                  onKeyUp={handleUsernameChange}
                  onKeyDown={handleStartTyping}
                  onBlur={formik.handleBlur}
                  value={formik.values.userName}
                />
                <div ref={spinner} className="spinner"></div>
                {formik.errors.userName ? (
                  <label className="text-danger ms-1">{formik.errors.userName}</label>
                ) : <label className="text-danger ms-1"></label>}
                <div ref={alert} className="alert">Username already exists</div>
              </div>

              <input
                type="password"
                name="password"
                spellCheck="false"
                className="control mt-0 mb-0"
                placeholder="Password"
                onChange={formik.handleChange}
                onKeyUp={handlePasswordChange}
                onBlur={formik.handleBlur}
                value={formik.values.password}
              />
              <div ref={bars} id="bars">
                <div></div>
              </div>
              {formik.errors.password ? (
                <label className="text-danger ms-1">{formik.errors.password}</label>
              ) : <label className="text-danger ms-1"></label>}

              <div>
                <input
                  type="retypePassword"
                  name="retypePassword"
                  className="control mb-0"
                  placeholder="Retype Password"
                  onChange={formik.handleChange}
                  onBlur={formik.handleBlur}
                  value={formik.values.retypePassword}
                />
                {formik.errors.retypePassword ? (
                  <label className="text-danger ms-1">{formik.errors.retypePassword}</label>
                ) : <label className="text-danger ms-1"></label>}
              </div>

              <button className="control" type="button" onClick={handleNextSlide}>Next</button>
            </SwiperSlide>

            <SwiperSlide>
              <div>
                <input
                  className="control mb-0"
                  type="email"
                  name="email"
                  placeholder="Email"
                  onChange={formik.handleChange}
                  onBlur={formik.handleBlur}
                  value={formik.values.email}
                />
                {formik.errors.email ? (
                  <label className="text-danger ms-1">{formik.errors.email}</label>
                ) : <label className="text-danger ms-1"></label>}
              </div>
              <div className="d-flex">
                <button className="control w-25 mx-2 mt-2" type="button" onClick={handlePrevSlide}>&larr;</button>
                <button className="control mt-2" type="button" onClick={handleNextSlide}>Next</button>
              </div>
            </SwiperSlide>


            <SwiperSlide>
              <div className="verification-code">
                <input className="code-input" maxLength="1" />
                <input className="code-input" maxLength="1" />
                <input className="code-input" maxLength="1" />
                <input className="code-input" maxLength="1" />
                <input className="code-input" maxLength="1" />
                <input className="code-input" maxLength="1" />
              </div>
              <button className="control" type="button">Submit Code</button>
            </SwiperSlide>
          </Swiper >

        </form>
      </div>
    </div>
  );
}
