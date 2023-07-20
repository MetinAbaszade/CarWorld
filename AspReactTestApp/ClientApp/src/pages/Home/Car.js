import React from 'react'

export default function Car() {
    return (
        <div className="m-3 w-72 bg-white border border-gray-200 rounded-lg shadow dark:bg-gray-800 dark:border-gray-700">
            <a className="w-100" href="#">
                <img className="hover:-translate-y-1 hover:scale-110 duration-200 w-100 h-72 rounded-t-lg object-cover" src="/CarImages/Car1.jpg" alt="" />
            </a>
            <div className='p-3'>
                <a href="#">
                    <h5 className=" text-2xl font-bold tracking-tight text-gray-700 dark:text-gray-400">BMW M5</h5>
                </a>
                <p className="mb-3 font-normal text-gray-700 dark:text-gray-400">18.07.2022</p>
                <a href="#" className="inline-flex items-center px-3 py-2 text-sm font-medium text-center text-white bg-blue-700 rounded-lg hover:bg-blue-800 focus:ring-4 focus:outline-none focus:ring-blue-300 dark:bg-blue-600 dark:hover:bg-blue-700 dark:focus:ring-blue-800">
                    Read more
                    <svg className="w-3.5 h-3.5 ml-2" aria-hidden="true" xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 14 10">
                        <path stroke="currentColor" strokeLinecap="round" strokeLinejoin="round" strokeWidth="2" d="M1 5h12m0 0L9 1m4 4L9 9" />
                    </svg>
                </a>
            </div>
        </div>
    );
}