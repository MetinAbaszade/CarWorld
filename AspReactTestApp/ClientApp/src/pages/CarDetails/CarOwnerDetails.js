// export default function CarOwnerDetails({ carDetails }) {
//   console.log(carDetails);

//   const convertCurrencyToAZN = (price, currency) => {
//     switch (currency) {
//       case "USD":
//         return (price * 1.7).toLocaleString("de-DE");
//       case "EUR":
//         return (price * 2).toLocaleString("de-DE");
//       default:
//         return price.toLocaleString("de-DE");
//     }
//   };

//   return (
//     <div className="w-75 bg-white flex-col p-4 mt-5 rounded-md">
//       <p className="text-xl">
//         {carDetails.brand} {carDetails.model}
//       </p>
//       <div className="flex flex-wrap justify-around mt-2">
//         <p className="text-xl">
//           {carDetails.price.toLocaleString("de-DE")} {carDetails.currency}
//         </p>
//         <svg
//           xmlns="http://www.w3.org/2000/svg"
//           fill="none"
//           viewBox="0 0 24 24"
//           strokeWidth="1.5"
//           stroke="currentColor"
//           className="w-6 h-6"
//         >
//           <path
//             strokeLinecap="round"
//             strokeLinejoin="round"
//             d="M7.5 21L3 16.5m0 0L7.5 12M3 16.5h13.5m0-13.5L21 7.5m0 0L16.5 12M21 7.5H7.5"
//           />
//         </svg>
//         <p className="text-xl">
//           {convertCurrencyToAZN(carDetails.price, carDetails.currency)} AZN
//         </p>
//       </div>

//       {carDetails.ownerName ? (
//         <div>
//           <p>{carDetails.ownerName}</p>
//           <p>{carDetails.ownerNumber}</p>
//         </div>
//       ) : (
//         <div>
//           <hr className="h-1 mx-auto mt-2 bg-gray-700 border-0 rounded"></hr>
//           <div className="flex items-center">
//             <img
//               className="w-14 h-14 mr-5"
//               src={`${carDetails.autoSalonLogoUrl}`}
//             ></img>
//             {carDetails.autoSalonTitle}
//           </div>
//           <hr className="h-1 mx-auto mb-2 bg-gray-700 border-0 rounded"></hr>
//           <p>Description: {carDetails.autoSalonDescription}</p>
//           <p>Location: {carDetails.autoSalonLocation}</p>
//           {/* Add more auto salon details or any other relevant information you wish to display */}
//         </div>
//       )}
//     </div>
//   );
// }

import React from "react";

export default function CarOwnerDetails({ carDetails }) {
  const convertCurrencyToAZN = (price, currency) => {
    switch (currency) {
      case "USD":
        return (price * 1.7).toLocaleString("de-DE");
      case "EUR":
        return (price * 2).toLocaleString("de-DE");
      default:
        return price.toLocaleString("de-DE");
    }
  };
  return (
    <div className="w-100 bg-white rounded-lg border border-gray-200 shadow-md overflow-hidden mt-5">
      <div className="p-5">
        <div className="flex justify-between items-center mb-3">
          <span className="text-3xl font-bold text-gray-900">
            {carDetails.price.toLocaleString("de-DE")} {carDetails.currency}
          </span>
          <span className="text-lg text-gray-600">
            ≈ {convertCurrencyToAZN(carDetails.price, carDetails.currency)} AZN
          </span>
        </div>
        <button className="flex items-center text-sm bg-green-500 hover:bg-green-700 text-white py-1 px-2 rounded-full mb-3">
          <svg
            xmlns="http://www.w3.org/2000/svg"
            fill="none"
            viewBox="0 0 24 24"
            stroke-width="1.5"
            stroke="currentColor"
            className="w-4 h-4 mr-1"
          >
            <path
              stroke-linecap="round"
              stroke-linejoin="round"
              d="M16.023 9.348h4.992v-.001M2.985 19.644v-4.992m0 0h4.992m-4.993 0 3.181 3.183a8.25 8.25 0 0 0 13.803-3.7M4.031 9.865a8.25 8.25 0 0 1 13.803-3.7l3.181 3.182m0-4.991v4.99"
            />
          </svg>
          Barter
        </button>
        <hr className="h-1 my-3 bg-gray-700 border-0 rounded"></hr>
        <div className="flex items-center mb-3">
          <img
            className="h-8 mr-3"
            src={`${carDetails.autoSalonLogoUrl}`}
            alt="AutoSalon Logo Placeholder"
          />
          <span className="text-lg font-semibold">{carDetails.autoSalonTitle}</span>
        </div>
        <button className="text-sm bg-green-500 hover:bg-green-700 text-white py-3 px-4 rounded mb-3 w-full flex items-center justify-center">
          <i className="fas fa-phone-alt pr-2"></i>
          <span>Nömrəni göstər</span>
        </button>
        <p className="text-gray-700 mb-3" dangerouslySetInnerHTML={{ __html: carDetails.autoSalonDescription }} />
        <div className="text-gray-600 text-sm mb-3">
          <span className="font-semibold">33 elan</span>
        </div>
        <div className="text-gray-600 text-sm mb-3">
          <i className="far fa-clock pr-1"></i>
          <span>Her gün: 09:00-18:00</span>
        </div>
        <div className="text-gray-600 text-sm">
          <a
            href={`${carDetails.autoSalonLocationUrl}`}
            className="fas fa-map-marker-alt pr-1"
            target="_blank"
            rel="noopener noreferrer"
          >
            <span>{carDetails.autoSalonLocation}</span>
          </a>
        </div>
        <div className="text-center mt-4">
          <a
            href="#"
            className="text-green-500 hover:text-green-700"
            target="_blank"
            rel="noopener noreferrer"
          >
            Salona keç
          </a>
        </div>
      </div>
    </div>
  );
}
