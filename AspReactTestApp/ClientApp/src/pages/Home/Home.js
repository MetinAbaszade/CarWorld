import { useState, useEffect } from 'react';
import Car from './Car'
import { Select } from '../../components/Select';
import { CarService } from "../../Services";
import { useNavigate } from "react-router-dom";

import { FueltypeService, BrandService, ColorService, TransmissionService, RegionService } from "../../Services";

export default function Home() {
  const [cars, setCars] = useState([]);
  const [brands, setBrands] = useState([]);
  const [colors, setColors] = useState([]);
  const [models, setModels] = useState([]);
  const [regions, setRegions] = useState([]);
  const [fueltypes, setFueltypes] = useState([]);
  const [transmissions, setTransmissions] = useState([]);
  const [isAuthorized, setIsAuthorized] = useState(null);

  const [selectedFueltype, setSelectedFueltype] = useState(0);
  const [selectedBrand, setSelectedBrand] = useState(0);

  const navigate = useNavigate();

  useEffect(() => {
    fetchData();
  }, []);

  async function fetchData() {
    try {
      const startTime = performance.now();
      const [brands, colors, regions, fuelTypes, transmissions, cars] = await Promise.all([
        BrandService.GetBrands(),
        ColorService.GetColors(),
        RegionService.GetRegions(),
        FueltypeService.GetFueltypes(),
        TransmissionService.GetTransmissions(),
        CarService.GetCars()
      ]);

      setBrands(brands);
      setColors(colors);
      setRegions(regions);
      setFueltypes(fuelTypes);
      setTransmissions(transmissions);
      setCars(cars);
      setIsAuthorized(true);  // NEW

      const duration = performance.now() - startTime;
      console.log(`someMethodIThinkMightBeSlow took ${duration}ms`);
    } catch (error) {
      console.log(error)
      if (error.status === 401) {
        navigate("/auth/login");
      }
    }
  }



  return (
    <div className="d-flex flex-col mx-2 mt-2 justify-content-center">
      <div className='d-flex flex-col py-3 bg-gray-900'>
        <div className='d-flex justify-end'>
          <button className="flex items-center bg-red-500 hover:bg-red-700 text-white font-bold py-2 px-4 rounded mx-3">
            <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" strokeWidth="1.5" stroke="currentColor" className="w-5 h-5 mx-3">
              <path strokeLinecap="round" strokeLinejoin="round" d="M21 21l-5.197-5.197m0 0A7.5 7.5 0 105.196 5.196a7.5 7.5 0 0010.607 10.607z" />
            </svg>
            Sadə Axtarış
          </button>
          <button className="flex items-center bg-red-500 hover:bg-red-700 text-white font-bold py-2 px-4 rounded mx-3">
            <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" strokeWidth="1.5" stroke="currentColor" className="w-6 h-6 mr-2">
              <path strokeLinecap="round" strokeLinejoin="round" d="M10.5 6h9.75M10.5 6a1.5 1.5 0 11-3 0m3 0a1.5 1.5 0 10-3 0M3.75 6H7.5m3 12h9.75m-9.75 0a1.5 1.5 0 01-3 0m3 0a1.5 1.5 0 00-3 0m-3.75 0H7.5m9-6h3.75m-3.75 0a1.5 1.5 0 01-3 0m3 0a1.5 1.5 0 00-3 0m-9.75 0h9.75" />
            </svg>
            Ətraflı Axtarış
          </button>
        </div>

        <div className="d-flex flex-wrap justify-center md:justify-around">
          <div className='mt-4 mx-2'>
            <Select
              label='Brand'
              onChange={setSelectedBrand}
              options={brands}
            />
          </div>

          <div className='mt-4 mx-2'>
            <Select
              label='Colors'
              onChange={setSelectedBrand}
              options={colors}
            />
          </div>

          <div className='mt-4 mx-2'>
            <Select
              label='Transmissions'
              onChange={setSelectedBrand}
              options={transmissions}
            />
          </div>

          <div className='mt-4 mx-2'>
            <Select
              label='Regions'
              onChange={setSelectedBrand}
              options={regions}
            />
          </div>
        </div>

        <div className='d-flex justify-end mt-4'>
          <button className="flex items-center text-red-500 border-2 border-red-500 bg-zinc-100 hover:bg-zinc-50 font-bold py-2 px-4 rounded mx-2">
            <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" strokeWidth="1.5" stroke="currentColor" className="w-5 h-5 mr-2">
              <path strokeLinecap="round" strokeLinejoin="round" d="M16.023 9.348h4.992v-.001M2.985 19.644v-4.992m0 0h4.992m-4.993 0l3.181 3.183a8.25 8.25 0 0013.803-3.7M4.031 9.865a8.25 8.25 0 0113.803-3.7l3.181 3.182m0-4.991v4.99" />
            </svg>
            Sıfırla
          </button>
          <button className="d-flex items-center bg-green-500 hover:bg-green-700 text-white font-bold py-2 px-4 rounded mx-3">
            <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" strokeWidth="1.5" stroke="currentColor" className="w-5 h-5 mx-2">
              <path strokeLinecap="round" strokeLinejoin="round" d="M21 21l-5.197-5.197m0 0A7.5 7.5 0 105.196 5.196a7.5 7.5 0 0010.607 10.607z" />
            </svg>
            Axtar
          </button>
        </div>
      </div>

      <div className="d-flex flex-wrap justify-content-around mt-4">
        {console.log(cars)}
        {cars.map((car, index) => (
          <Car key={index} car={car} />
        ))}
      </div>
    </div>
  );
}