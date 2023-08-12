import React from "react";
import '../Auth.css';
import LoginCard from "./LoginCard/LoginCard";
import Carousel from "../Carousel/CarouselCompenent";

export default function Login() {
  return (
    <div className="login-primary-container">
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
