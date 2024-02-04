import TextTranslator from "../../languages/texttranslator";

export default function CarDetailTable({ carDetails }) {
  return (
    <div className="w-100 mt-4 bg-white flex-col p-4  overflow-hidden rounded-lg border border-gray-200 shadow-md mx-5">
      <div className="flex justify-between gap-x-2">
        <label> {TextTranslator("releaseyear")}:</label>
        <p>{carDetails.releaseYear}</p>
      </div>

      <hr className="h-1 mx-auto my-2 bg-gray-700 border-0 rounded"></hr>

      <div className="flex justify-between">
        <label>{TextTranslator("region")}: </label>
        <p>{carDetails.region}</p>
      </div>

      <hr className="h-1 mx-auto my-2 bg-gray-700 border-0 rounded"></hr>

      <div className="flex justify-between">
        <label>{TextTranslator("mileage")}:</label>
        <p>
          {carDetails.mileage} {carDetails.mileageType}
        </p>
      </div>

      <hr className="h-1 mx-auto my-2 bg-gray-700 border-0 rounded"></hr>

      <div className="flex justify-between">
        <label>{TextTranslator("category")}:</label>
        <p>{carDetails.category}</p>
      </div>

      <hr className="h-1 mx-auto my-2 bg-gray-700 border-0 rounded"></hr>

      <div className="flex justify-between">
        <label>{TextTranslator("color")}:</label>
        <p>{carDetails.color}</p>
      </div>

      <hr className="h-1 mx-auto my-2 bg-gray-700 border-0 rounded"></hr>

      <div className="flex justify-between">
        <label>{TextTranslator("engine")}:</label>
        <p>
          {carDetails.engineVolume}L / {carDetails.horsePower} hp / {carDetails.fuelType}
        </p>
      </div>

      <hr className="h-1 mx-auto my-2 bg-gray-700 border-0 rounded"></hr>

      <div className="flex justify-between">
        <label>{TextTranslator("transmission")}:</label>
        <p>{carDetails.gearType}</p>
      </div>

      <hr className="h-1 mx-auto my-2 bg-gray-700 border-0 rounded"></hr>

      <div className="flex justify-between">
        <label>{TextTranslator("market")}:</label>
        <p>{carDetails.market}</p>
      </div>
    </div>
  );
}
