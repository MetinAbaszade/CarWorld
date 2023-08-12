import { configureStore } from "@reduxjs/toolkit";
import languagereducer from './languageslice'

const store = configureStore({
    reducer: {
        languagereducer
    }
})

export default store;