import React, { useEffect } from "react";
import '../Auth.css';
import LoginCard from "./LoginCard/LoginCard";
import Carousel from "../Carousel/CarouselCompenent";

export default function Login() {

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
