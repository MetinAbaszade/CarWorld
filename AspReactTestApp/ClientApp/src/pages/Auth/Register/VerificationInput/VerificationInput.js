import React, { useState } from 'react';
import './VerificationInput.css'

export default function VerificationInput({ handlePrevSlide, formik }) {
    const [isLoading, setIsLoading] = useState(false);
    const inputRefs = Array.from({ length: 6 }, () => React.createRef());

    const handleInput = (e, index, nextInput) => {
        // Checking for number
        e.target.value = e.target.value.replace(/[^0-9]/g, '');

        const verificationCode = [...formik.values.verificationCode];
        verificationCode[index] = e.target.value;
        formik.setFieldValue('verificationCode', verificationCode);

        if (e.target.value.length === 1 && nextInput) {
            nextInput.focus();
        }
        if (e.target.value.length === 1 && nextInput === undefined) {
            e.target.blur();
        }
    };

    const handleKeyDown = (e, prevInput) => {
        if (e.key === 'Backspace' && prevInput && !e.target.value) {
            prevInput.focus();
        }
    };

    async function SubmitButtonClick() {
        setIsLoading(true);
        await formik.submitForm();
        setIsLoading(false);
    }

    return (
        <div className='d-flex flex-column'>
            <div className="verification-code mt-2">
                {inputRefs.map((inputRef, index) => (
                    <input
                        key={index}
                        ref={inputRef}
                        className="code-input"
                        maxLength="1"
                        value={formik.values.verificationCode[index]}
                        onChange={(e) => handleInput(e, index, inputRefs[index + 1]?.current)}
                        onKeyDown={(e) => handleKeyDown(e, inputRefs[index - 1]?.current)}
                    />
                ))}
            </div>
            <label className={`text-danger text-center ${formik.errors.verificationCode ? "visible mt-3" : ""}`}>
                {formik.errors.verificationCode}
            </label>
            <div className="d-flex">
                <button className="control w-25 mx-2 mt-4" type="button" onClick={handlePrevSlide}>&larr;</button>
                <button className="control mt-4 disabled" type="button" onClick={SubmitButtonClick}>
                {isLoading ? (
                    <div className="spinner visible text-center m-auto"></div>
                ) : (
                    "Submit Code"
                )}
               </button>
            </div>
        </div>
    );
}
