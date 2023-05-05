import React, { useState, useRef, useCallback } from "react";
import { useNavigate } from "react-router-dom";
import { AuthService } from '../../../Services';
import { Swiper, SwiperSlide } from 'swiper/react';
import SwiperCore, { Navigation } from 'swiper/core';
import { useFormik } from 'formik';
import * as Yup from 'yup';

import 'react-responsive-carousel/lib/styles/carousel.min.css';
import 'swiper/css';
import 'swiper/css/navigation';

export default function RegisterCard() {

    const errorLabel = useRef();
    const spinner = useRef();
    const bars = useRef();
    const profileImg = useRef(null);
    const navigate = useNavigate();
    const [selectedImage, setSelectedImage] = useState(null);

    SwiperCore.use([Navigation]);


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

            let name = values.name
            let surname = values.surname;
            let username = values.userName
            let email = values.userName
            let password = values.password;
            let retypePassword = values.retypePassword;


            let authResponse = await AuthService.Register(name, surname, username, email, selectedImage, password, retypePassword);

            if (authResponse.isSuccessfull) {
                navigate("/Auth/Login");
            }
            else {
                console.log(errorLabel);
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

        if (value === "Matin") {
            formik.setFieldError("userName", "Username already exists");

            //alert.current.classList.add("visible");
        } else {
            formik.errors.email = ""
            // alert.current.classList.remove("visible");
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

    //#region UploadImage
    const handleImageChange = (e) => {
        if (e.target.files && e.target.files[0]) {
            setSelectedImage(e.target.files[0]);
            const reader = new FileReader();
            reader.onload = function (e) {
                document.getElementById('profileImage').src = e.target.result;
            };
            reader.readAsDataURL(e.target.files[0]);
        }
    };
    //#endregion

    return (
        <form className="login-form" onSubmit={formik.handleSubmit}>
            <div className="image-container">
                <img ref={profileImg} src="/UserLogo.png" alt="Profile Image" id="profileImage" />
                <div className="upload-overlay" onClick={() => { document.getElementById('fileInput').click(); }}>
                    <p className="upload-text">Upload Photo</p>
                </div>
                <input type="file" id="fileInput" accept="image/*" style={{ "display": "none" }} onChange={handleImageChange} />
            </div>
            <h2 className="text-center">Create Account</h2>
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
                            className="control"
                            type="name"
                            name="name"
                            placeholder="Name"
                            onChange={formik.handleChange}
                            onBlur={formik.handleBlur}
                            value={formik.values.name}
                        />
                        <label className={`text-danger ${formik.errors.name ? "visible" : ""}`}>
                            {formik.errors.name}
                        </label>
                    </div>

                    <div className={`${formik.errors.name ? "" : "mt-3"}`}>
                        <input
                            type="surname"
                            name="surname"
                            className="control"
                            placeholder="Surname"
                            onChange={formik.handleChange}
                            onBlur={formik.handleBlur}
                            value={formik.values.surname}
                        />
                        <label className={`text-danger mt-1 ${formik.errors.surname ? "visible" : ""}`}>
                            {formik.errors.surname}
                        </label>
                    </div>

                    <div className={`username ${formik.errors.surname ? "" : "mt-3"}`}>
                        <input
                            type="userName"
                            name="userName"
                            spellCheck="false"
                            className="control"
                            placeholder="Username"
                            onChange={formik.handleChange}
                            onKeyUp={handleUsernameChange}
                            onKeyDown={handleStartTyping}
                            onBlur={formik.handleBlur}
                            value={formik.values.userName}
                        ></input>
                        <div ref={spinner} className="spinner"></div>
                        <label className={`text-danger ms-1 ${formik.errors.userName ? "visible" : ""}`}>
                            {formik.errors.userName}
                        </label>
                    </div>

                    <div>
                        <input
                            type="password"
                            name="password"
                            spellCheck="false"
                            className="control"
                            placeholder="Password"
                            onChange={formik.handleChange}
                            onKeyUp={handlePasswordChange}
                            onBlur={formik.handleBlur}
                            value={formik.values.password}
                        />
                        <div ref={bars} id="bars">
                            <div></div>
                        </div>
                        <label className={`text-danger ms-1 ${formik.errors.password ? "visible" : ""}`}>
                            {formik.errors.password}
                        </label>
                    </div>

                    <div>
                        <div>
                            <input
                                type="retypePassword"
                                name="retypePassword"
                                className="control"
                                placeholder="Retype Password"
                                onChange={formik.handleChange}
                                onBlur={formik.handleBlur}
                                value={formik.values.retypePassword}
                            />
                            <label className={`text-danger ms-1 ${formik.errors.retypePassword ? "visible" : ""}`}>
                                {formik.errors.retypePassword}
                            </label>
                        </div>
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
                        <label className={`text-danger ms-1 ${formik.errors.email ? "visible" : ""}`}>
                            {formik.errors.email}
                        </label>
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
                    <div className="d-flex">
                        <button className="control w-25 mx-2 mt-2" type="button" onClick={handlePrevSlide}>&larr;</button>
                        <button className="control" type="button">Submit Code</button>
                    </div>

                </SwiperSlide>
            </Swiper >

        </form>
    )
}
