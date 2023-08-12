

export default function CarOwnerDetails({ carDetails }) {

    const convertCurrencyToAZN = (price, currency) => {
        switch (currency) {
            case 'USD':
                return (price * 1.7).toLocaleString('de-DE');
            case 'EUR':
                return (price * 2).toLocaleString('de-DE');
            default:
                return (price).toLocaleString('de-DE');
        }
    }

    return (
        <div className='w-75 bg-white flex-col p-4 mt-5 rounded-md'>
            <p className='text-xl'>{carDetails.brand}  {carDetails.model}</p>
            <div className='flex flex-wrap justify-around mt-2'>
                <p className='text-xl'>{carDetails.price.toLocaleString('de-DE')} {carDetails.currency}</p>
                <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" strokeWidth="1.5" stroke="currentColor" className="w-6 h-6">
                    <path strokeLinecap="round" strokeLinejoin="round" d="M7.5 21L3 16.5m0 0L7.5 12M3 16.5h13.5m0-13.5L21 7.5m0 0L16.5 12M21 7.5H7.5" />
                </svg>
                <p className='text-xl'>{convertCurrencyToAZN(carDetails.price, carDetails.currency)} AZN</p>
            </div>

            {carDetails.ownerName && <p>{carDetails.ownerName}</p>}

            <hr className="h-1 mx-auto my-2 bg-gray-700 border-0 rounded"></hr>

            <div className='flex justify-between'>
                <label>Satış şəhəri:</label>
                <p>{carDetails.region}</p>
            </div>

            <hr className="h-1 mx-auto my-2 bg-gray-700 border-0 rounded"></hr>

            <div className='flex justify-between'>
                <label>Yürüş:</label>
                <p>{carDetails.mileage.toLocaleString('de-DE')} {carDetails.mileageType}</p>
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