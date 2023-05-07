import React, { useRef } from 'react' 

export default function StrengthBar({ password }) {
    const bars = useRef(null);

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

    const strengthText = getStrength(password);

  return (
      <div ref={bars} id="bars" className={`${strengthText} mt-1 mb-0` }>
          <div></div>
      </div>
  )
}
