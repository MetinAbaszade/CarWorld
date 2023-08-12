import React, { useState, useRef, useCallback } from "react";
import { AuthService } from '../../../../Services';
import { Swiper, SwiperSlide } from 'swiper/react';
import SwiperCore, { Navigation } from 'swiper/core';
import useRegisterFormValidation from "../../Validations/useRegisterFormValidation";
import ProfileImageComponent from '../ProfileImage/ProfileImageComponent'

import 'react-responsive-carousel/lib/styles/carousel.min.css';
import 'swiper/css';
import 'swiper/css/navigation';
import StrengthBar from "../StrengthBar/StrengthBarComponent";
import VerificationInput from "../VerificationInput/VerificationInput"; 
import { NavLink } from "react-router-dom";


export default function RegisterCard() {
    const spinnerRef = useRef();
    const swiperRef = useRef();
    const [selectedImage, setSelectedImage] = useState(null);
    const [loading, setLoading] = useState(false);
    SwiperCore.use([Navigation]);

    const { formik, FirstPageSchema, SecondPageSchema, validateForm } = useRegisterFormValidation(selectedImage);

    //#region spinner

    const debounce = (callback, time) => {
        let interval;
        return (...args) => {
            clearTimeout(interval);
            interval = setTimeout(() => {
                callback.apply(null, args);
            }, time);
        };
    };

    
    const checkUsernameExistsandUpdateUI = async (userName) => {
        spinnerRef.current.classList.remove("visible");
        const userExists = await AuthService.CheckUserExists(userName);
        if (userExists) {
            formik.setFieldError("userName", "Username already exists");
            return true;
        }
        else {
            formik.setFieldError("userName", "");
            return false;
        }
    };

    const handleUsernameChange = useCallback(
        debounce(async (input) => {
            spinnerRef.current.classList.remove("visible");
            const { value } = input.target;
            formik.values.userName = value;
            await checkUsernameExistsandUpdateUI(value);
        }, 500),
        [formik]
    );

    const handleStartTyping = () => {
        spinnerRef.current.classList.add("visible");
    };

    //#endregion

    //#region Swiper

    const handleNextSlide = async () => {
        const activeIndex = swiperRef.current.activeIndex;
        let schema, valuesToValidate, valid;

        // For each page, we have different schemas and values to validate
        if (activeIndex === 0) {
            console.log(formik.errors)
            // If Username exists, then immediately return
            if (await checkUsernameExistsandUpdateUI(formik.values.userName)) {
                return;
            }
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
            if (activeIndex === 1) {
                setLoading(true);
                const sendVerificationResponse = await AuthService.SendVerificationCode(formik.values.email);
                setLoading(false);

                if (!sendVerificationResponse.isSuccessfull) {
                    const errorModel = sendVerificationResponse.errors;
                    console.log(errorModel)
                    formik.setErrors(errorModel);
                    return;
                }
            }
            swiperRef.current.slideNext();
            return;
        }
    };

    const handlePrevSlide = () => {
        if (swiperRef.current) {
            const activeIndex = swiperRef.current.activeIndex;

            if (activeIndex === 2) {
                formik.setFieldValue('verificationCode', new Array(6).fill(''));
            }
            swiperRef.current.slidePrev();
        }
    };

    //#endregion

    return (
        <form className="login-form" onSubmit={formik.handleSubmit}>
            <ProfileImageComponent setSelectedImage={setSelectedImage} />

            <h2 className="text-center font-mono text-2xl">Create Account</h2>
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
                        <div ref={spinnerRef} className="spinner"></div>
                        <label className={`text-danger ${formik.errors.userName ? "visible" : ""}`}>
                            {formik.errors.userName}
                        </label>
                    </div>

                    <div className={`${formik.errors.userName ? "" : "mt-3"}`}>
                        <input
                            type="password"
                            name="password"
                            spellCheck="false"
                            className="control"
                            placeholder="Password"
                            onChange={formik.handleChange}
                            onBlur={formik.handleBlur}
                            value={formik.values.password}
                        />
                        <label className={`text-danger ${formik.errors.password ? "visible" : ""}`}>
                            {formik.errors.password}
                        </label>
                        <StrengthBar password={formik.values.password} />
                    </div>

                    <div className={`${formik.errors.password ? "" : "mt-3"}`}>
                        <div>
                            <input
                                type="password"
                                name="retypePassword"
                                className="control"
                                placeholder="Retype Password"
                                onChange={formik.handleChange}
                                onBlur={formik.handleBlur}
                                value={formik.values.retypePassword}
                            />
                            <label className={`text-danger ${formik.errors.retypePassword ? "visible" : ""}`}>
                                {formik.errors.retypePassword}
                            </label>
                        </div>
                    </div>

                    <button className="control" type="button" onClick={handleNextSlide}>Next</button>
                    <div className="text-center mt-1">
                        <NavLink to="/auth/login" className="text-md font-bold hover:text-red-600">Already have an account</NavLink>
                    </div>
                </SwiperSlide>

                <SwiperSlide>
                    <div className="mt-4">
                        <input
                            className="control mb-0"
                            type="email"
                            name="email"
                            placeholder="Email"
                            disabled={loading}
                            onChange={formik.handleChange}
                            onBlur={formik.handleBlur}
                            value={formik.values.email}
                        />
                    </div>
                    <label className={`text-danger ps-2${formik.errors.email ? "visible" : ""}`}>
                        {formik.errors.email}
                    </label>
                    <div className="d-flex">
                        <button className="control w-25 ms-1 me-2 mt-2" type="button" disabled={loading} onClick={handlePrevSlide}>&larr;</button>
                        <button className="control mt-2 text-center" type="button" disabled={loading} onClick={handleNextSlide}>
                            {loading ? (
                                <div className="spinner visible text-center m-auto"></div>
                            ) : (
                                "Next"
                            )}
                        </button>
                    </div>
                </SwiperSlide>

                <SwiperSlide>
                    <VerificationInput handlePrevSlide={handlePrevSlide} formik={formik} />
                </SwiperSlide>
            </Swiper >
        </form>
    )
}
