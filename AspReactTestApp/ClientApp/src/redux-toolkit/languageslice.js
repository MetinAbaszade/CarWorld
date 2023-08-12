import { createSlice } from "@reduxjs/toolkit";
import { dictionaryList } from '../languages';
import { getCookie } from "../Services/CookieService";

var default_language_id = 1;

const language_id_in_cookie = getCookie("languageId");
if (language_id_in_cookie && language_id_in_cookie.trim() !== "") {
    default_language_id = language_id_in_cookie;
} 

const initialState = {
    selectedLanguageId: default_language_id,
    dictionary: dictionaryList[default_language_id]
}

const languages = createSlice({
    name: 'languages',
    initialState: initialState,
    reducers: {
        changeLanguage: (state, action) => {
            state.selectedLanguageId = action.payload;
            state.dictionary = dictionaryList[action.payload];
        }
    }
})

export const { changeLanguage } = languages.actions
export default languages.reducer