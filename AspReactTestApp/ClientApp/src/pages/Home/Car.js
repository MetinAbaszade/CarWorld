import React from 'react'
import { Link } from 'react-router-dom';

export default function Car({ car }) {
    return (
        <Link to={`/car/${car.id}`}>
            <div className="m-3 w-72 bg-white border border-gray-200 rounded-lg shadow dark:bg-gray-800 dark:border-gray-700">
                <img className="hover:-translate-y-1 hover:scale-110 duration-200 w-100 h-72 rounded-t-lg object-cover" src={"/Images/" + car.coverImageUrl} alt="" />
                <div className='p-3'>
                    <p className="text-xl mb-1 font-semibold text-slate-950">{car.price} {car.currency}</p>
                    <h5 className="text-lg tracking-tight text-slate-950">{car.brand} {car.model}</h5>
                    <p className=" text-base font-normal text-slate-950">{car.releaseYear}, {car.engineVolume}L, {car.mileage} {car.mileageType}</p>
                    <p className="mb-3 text-base font-normal text-slate-950">{car.region}</p>
                </div>
            </div>
        </Link>
    );
}