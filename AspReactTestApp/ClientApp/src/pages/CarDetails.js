import { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import { CarService } from '../Services';

export default function CarDetails() {
    const { id } = useParams();
    const [carDetails, setCarDetails] = useState();

    async function fetchdata() {
        var result = await CarService.GetCarDetails(id);
        setCarDetails(result);
        console.log(result);
    }

    useEffect(() => {
        fetchdata();
    }, [])

    return (
        <p>{id}</p>
    );
}