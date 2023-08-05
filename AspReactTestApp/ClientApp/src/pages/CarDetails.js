import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import { CarService } from '../Services';
import AliceCarousel from 'react-alice-carousel';
import 'react-alice-carousel/lib/alice-carousel.css';

export default function CarDetails() {
    const { id } = useParams();
    const [carDetails, setCarDetails] = useState();
    const [isLoading, setIsLoading] = useState(true);

    async function fetchdata() {
        setIsLoading(true);
        try {
            const result = await CarService.GetCarDetails(id);
            console.log(result);
            setCarDetails(result);
        } catch (error) {
            console.error('Error fetching car details:', error);
        } finally {
            setIsLoading(false);
        }
    }

    useEffect(() => {
        fetchdata();
    }, []);

    if (isLoading) {
        return <h1>Loading...</h1>;
    }


    return (
        <div className='flex bg-gray-200'>
            <div className='p-5 flex flex-col w-3/5'>
                <div className='bg-gray-700 pb-0 pt-4 rounded-sm'>
                    {carDetails.images && carDetails.images.$values && carDetails.images.$values.length > 0 ? (
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
                                className="w-100"
                                renderPrevButton={() => {
                                    return <svg className="p-4 absolute left-0 top-1/2 transform -translate-y-1/2 w-1/5 h-1/5" xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke-width="1.5" stroke="currentColor" >
                                        <path strokeLinecap="round" strokeLinejoin="round" d="M15.75 19.5L8.25 12l7.5-7.5" />
                                    </svg>

                                }}
                                renderNextButton={() => {
                                    return <svg className="p-4 absolute right-0 top-1/2 transform -translate-y-1/2 w-1/5 h-1/5" xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke-width="1.5" stroke="currentColor">
                                        <path strokeLinecap="round" strokeLinejoin="round" d="M8.25 4.5l7.5 7.5-7.5 7.5" />
                                    </svg>
                                }}
                            >
                                {carDetails.images.$values.map((image, index) => (
                                    <img
                                        src={"/Images/" + image}
                                        className="mx-auto"
                                        key={index}
                                        alt={`Car Image ${index}`}
                                    />
                                ))}
                            </AliceCarousel>
                        </div>
                    ) : (
                        <p>No images available</p>
                    )}
                </div>
                <div className='flex gap-x-2 mt-5'>
                    <label>Description:</label>
                    <p>{carDetails.description}</p>
                </div>
            </div>
            <div className='flex flex-col ms-4  items-center flex-grow'>
                <div className='w-75 bg-white flex-col p-4 mt-5 rounded-md'>
                    <div className='flex justify-between gap-x-2'>
                        <label>Buraxılış ili:</label>
                        <p>{carDetails.releaseYear}</p>
                    </div>

                    <hr class="h-1 mx-auto my-2 bg-gray-700 border-0 rounded"></hr>

                    <div className='flex justify-between'>
                        <label>Satış şəhəri:</label>
                        <p>{carDetails.region}</p>
                    </div>

                    <hr class="h-1 mx-auto my-2 bg-gray-700 border-0 rounded"></hr>

                    <div className='flex justify-between'>
                        <label>Yürüş:</label>
                        <p>{carDetails.mileage} {carDetails.mileageType}</p>
                    </div>

                    <hr class="h-1 mx-auto my-2 bg-gray-700 border-0 rounded"></hr>

                    <div className='flex justify-between'>
                        <label>Ban növü:</label>
                        <p>{carDetails.category}</p>
                    </div>

                    <hr class="h-1 mx-auto my-2 bg-gray-700 border-0 rounded"></hr>

                    <div className='flex justify-between'>
                        <label>Rəng:</label>
                        <p>{carDetails.color}</p>
                    </div>

                    <hr class="h-1 mx-auto my-2 bg-gray-700 border-0 rounded"></hr>

                    <div className='flex justify-between'>
                        <label>Mühərrik:</label>
                        <p>{carDetails.engineVolume}L / {carDetails.horsePower} hp</p>
                    </div>

                    <hr class="h-1 mx-auto my-2 bg-gray-700 border-0 rounded"></hr>

                    <div className='flex justify-between'>
                        <label>Ötürücü:</label>
                        <p>{carDetails.gearType}</p>
                    </div>

                    <hr class="h-1 mx-auto my-2 bg-gray-700 border-0 rounded"></hr>

                    <div className='flex justify-between'>
                        <label>Brand:</label>
                        <p>{carDetails.brand}</p>
                    </div>

                    <hr class="h-1 mx-auto my-2 bg-gray-700 border-0 rounded"></hr>

                    <div className='flex justify-between'>
                        <label>Model:</label>
                        <p>{carDetails.model}</p>
                    </div>

                    <hr class="h-1 mx-auto my-2 bg-gray-700 border-0 rounded"></hr>

                    <div className='flex justify-between'>
                        <label>Qiymət:</label>
                        <p>{carDetails.price} {carDetails.currency}</p>
                    </div>

                    <hr class="h-1 mx-auto my-2 bg-gray-700 border-0 rounded"></hr>

                    <div className='flex justify-between'>
                        <label>Market:</label>
                        <p>{carDetails.market}</p>
                    </div>

                    <hr class="h-1 mx-auto my-2 bg-gray-700 border-0 rounded"></hr>

                    <div className='flex justify-between'>
                        <label>Yanacağ Növü:</label>
                        <p>{carDetails.fuelType}</p>
                    </div>
                </div>
            </div>
        </div>
    );
}
