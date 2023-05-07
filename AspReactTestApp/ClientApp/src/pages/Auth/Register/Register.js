import Carousel from './Carousel/CarouselCompenent';
import './Register.css';
import RegisterCard from './RegisterCard/RegisterCard';


export default function Register() {


  return (
    <div className="primary-container">
      <div className="secondary-container">
        <div className="left">
          <Carousel></Carousel>
        </div>
        <div className="right login-card">
          <RegisterCard></RegisterCard>
        </div>
      </div>
    </div>
  );
}
