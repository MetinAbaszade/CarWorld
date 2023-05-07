import React, { useState } from 'react';
import './VerificationInput.css'

export default function VerificationInput({ handlePrevSlide, seetVerificationCode }) {
    const inputRefs = Array.from({ length: 6 }, () => React.createRef());

    const handleInput = (e, index, nextInput) => {
        // Checking for number
        e.target.value = e.target.value.replace(/[^0-9]/g, '');

        const newCode = [...code];
        newCode[index] = e.target.value;
        setCode(newCode);

        if (e.target.value.length === 1 && nextInput) {
            nextInput.focus();
        }
        if (e.target.value.length === 1 && nextInput === undefined) {
            e.target.blur();
        }
    };

    const handleKeyDown = (e, prevInput, nextInput) => {
        if (e.key === 'Backspace' && prevInput && !e.target.value) {
            prevInput.focus();
        }
    };

    const handleSubmit = (e) => {
        e.preventDefault();
        const codeString = code.join('');
        console.log('Submitted code:', codeString);
        // You can use codeString for your desired action here
    };

    return (
        <>
            <div className="verification-code">
                {inputRefs.map((inputRef, index) => (
                    <input
                        key={index}
                        ref={inputRef}
                        className="code-input"
                        maxLength="1"
                        value={code[index]}
                        onChange={(e) => handleInput(e, index, inputRefs[index + 1]?.current)}
                        onKeyDown={(e) => handleKeyDown(e, inputRefs[index - 1]?.current, inputRefs[index + 1]?.current)}
                    />
                ))}
            </div>
            <div className="d-flex">
                <button className="control w-25 mx-2 mt-4" type="button" onClick={handlePrevSlide}>&larr;</button>
                <button className="control" type="submit">Submit Code</button>
            </div></>
    );
}
