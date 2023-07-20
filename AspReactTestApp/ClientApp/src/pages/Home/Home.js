import React, { useEffect, useState } from "react";
import Car from "./Car";
import { FueltypeService, BrandService, ColorService, TransmissionService, RegionService } from "../../Services";

export default function Home() {
  const [cars, setCars] = useState([1, 2, 3, 4, 5, 6, 7, 8, 9, 10]);
  const [brands, setBrands] = useState([]);
  const [colors, setColors] = useState([]);
  const [models, setModels] = useState([]);
  const [regions, setRegions] = useState([]);
  const [fueltypes, setFueltypes] = useState([]);
  const [transmissions, setTransmissions] = useState([]);

  async function fetchData() {
    try {
      const [brands, colors, regions, fuelTypes, transmissions] = await Promise.all([
        BrandService.GetBrands(),
        ColorService.GetColors(),
        RegionService.GetRegions(),
        FueltypeService.GetFueltypes(),
        TransmissionService.GetTransmissions(),
      ]);

      setBrands(brands);
      setColors(colors);
      setRegions(regions);
      setFueltypes(fuelTypes);
      setTransmissions(transmissions);
    } catch (error) {
      console.error('Error:', error.message);
    }
  }

  useEffect(() => {
    fetchData();
  }, []);

  return (
    <div className="d-flex flex-column mx-2">
      <div className="d-flex justify-content-around">
        <div>
          <select id="brands" className="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500">
            <option selected>Choose Brand</option>
            {
              brands.map((brand, index) => (
                <option key={index}>{brand.name}</option>
              ))
            }
          </select>
        </div>

        <div>
          <select id="fueltype" className="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500">
            <option selected>Choose Fueltype</option>
            {
              fueltypes.map((fueltype, index) => (
                <option key={index}>{fueltype.fueltypeLocales[0]?.name}</option>
              ))
            }
          </select>
        </div>

        <div>
          <select id="colors" className="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500">
            <option selected>Choose Color</option>
            {
              colors.map((color, index) => (
                <option key={index}>{color.colorLocales[0]?.name}</option>
              ))
            }
          </select>
        </div>

        <div>
          <select id="transmissions" className="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500">
            <option selected>Choose Transmission</option>
            {
              transmissions.map((transmission, index) => (
                <option key={index}>{transmission.transmissionLocales[0]?.name}</option>
              ))
            }
          </select>
        </div>

        <div>
          <select id="regions" className="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500">
            <option selected>Choose Region</option>
            {
              regions.map((region, index) => (
                <option key={index}>{region.regionLocales[0]?.name}</option>
              ))
            }
          </select>
        </div>
      </div>

      <div className="d-flex flex-wrap justify-content-around mt-4">
        {cars.map((car, index) => (
          <Car key={index} />
        ))}
      </div>
    </div>
  );
}
