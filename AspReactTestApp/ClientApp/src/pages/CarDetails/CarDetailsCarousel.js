import AliceCarousel from "react-alice-carousel";
import 'react-alice-carousel/lib/alice-carousel.css';

export default function CarDetails({images}) { 

    return (
        <div className='w-100 h-100 flex items-center justify-center'>
            <AliceCarousel
                buttonsDisabled
                dotsDisabled
                showSlideInfo={false}
                autoPlay={true}
                infinite={true}
                autoPlayInterval={2000}
                fadeOutAnimation
                touchTracking
                className="w-100 carousel-wrapper"
                renderPrevButton={() => {
                    return (
                        <svg
                            className="p-4 absolute left-0 top-1/2 transform -translate-y-1/2 w-1/5 h-1/5"
                            xmlns="http://www.w3.org/2000/svg"
                            fill="none"
                            viewBox="0 0 24 24"
                            strokeWidth="1.5"
                            stroke="currentColor"
                        >
                            <path
                                strokeLinecap="round"
                                strokeLinejoin="round"
                                d="M15.75 19.5L8.25 12l7.5-7.5"
                            />
                        </svg>
                    );
                }}
                renderNextButton={() => {
                    return (
                        <svg
                            className="p-4 absolute right-0 top-1/2 transform -translate-y-1/2 w-1/5 h-1/5"
                            xmlns="http://www.w3.org/2000/svg"
                            fill="none"
                            viewBox="0 0 24 24"
                            strokeWidth="1.5"
                            stroke="currentColor"
                        >
                            <path
                                strokeLinecap="round"
                                strokeLinejoin="round"
                                d="M8.25 4.5l7.5 7.5-7.5 7.5"
                            />
                        </svg>
                    );
                }}
            >
                {images.map((image, index) => (
                    <div className="carousel-image-container">
                        <img
                            src={"/Images/" + image}
                            className="carousel-image-item rounded-md"
                            key={index}
                            alt={`Car Image ${index}`}
                        />
                    </div>
                ))}
            </AliceCarousel>
        </div>
    )
}