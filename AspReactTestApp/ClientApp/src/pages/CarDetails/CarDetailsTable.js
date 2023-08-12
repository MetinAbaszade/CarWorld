
export default function CarDetailTable({ carDetails }) {
    return (
        <div className='w-75 bg-white flex-col p-4 mt-5 rounded-md'>
            <div className='flex justify-between gap-x-2'>
                <label>Buraxılış ili:</label>
                <p>{carDetails.releaseYear}</p>
            </div>

            <hr className="h-1 mx-auto my-2 bg-gray-700 border-0 rounded"></hr>

            <div className='flex justify-between'>
                <label>Satış şəhəri:</label>
                <p>{carDetails.region}</p>
            </div>

            <hr className="h-1 mx-auto my-2 bg-gray-700 border-0 rounded"></hr>

            <div className='flex justify-between'>
                <label>Yürüş:</label>
                <p>{carDetails.mileage} {carDetails.mileageType}</p>
            </div>

            <hr className="h-1 mx-auto my-2 bg-gray-700 border-0 rounded"></hr>

            <div className='flex justify-between'>
                <label>Ban növü:</label>
                <p>{carDetails.category}</p>
            </div>

            <hr className="h-1 mx-auto my-2 bg-gray-700 border-0 rounded"></hr>

            <div className='flex justify-between'>
                <label>Rəng:</label>
                <p>{carDetails.color}</p>
            </div>

            <hr className="h-1 mx-auto my-2 bg-gray-700 border-0 rounded"></hr>

            <div className='flex justify-between'>
                <label>Mühərrik:</label>
                <p>{carDetails.engineVolume}L / {carDetails.horsePower} hp</p>
            </div>

            <hr className="h-1 mx-auto my-2 bg-gray-700 border-0 rounded"></hr>

            <div className='flex justify-between'>
                <label>Ötürücü:</label>
                <p>{carDetails.gearType}</p>
            </div>

            <hr className="h-1 mx-auto my-2 bg-gray-700 border-0 rounded"></hr>

            <div className='flex justify-between'>
                <label>Market:</label>
                <p>{carDetails.market}</p>
            </div>

            <hr className="h-1 mx-auto my-2 bg-gray-700 border-0 rounded"></hr>

            <div className='flex justify-between'>
                <label>Yanacağ Növü:</label>
                <p>{carDetails.fuelType}</p>
            </div>
        </div>
    )
}