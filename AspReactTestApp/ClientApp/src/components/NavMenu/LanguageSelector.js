import { languageOptions } from '../../languages';
import { changeLanguage } from '../../redux-toolkit/languageslice';
import store from '../../redux-toolkit';
import { useSelector } from "react-redux";
import { setCookie } from '../../Services/CookieService';

export default function LanguageSelector() {
    const { selectedLanguageId } = useSelector((state) => state.languagereducer);
    function handleLanguageChange(e) {
        var languageId = e.target.value;
        var cookieExpireTimeInDays = 364; // one year
        setCookie("languageId", languageId, cookieExpireTimeInDays);
        store.dispatch(changeLanguage(e.target.value));
    } 
    return (
        <div>
            <select
                onChange={handleLanguageChange}
                value={selectedLanguageId}
            >
                {languageOptions.map((language) => (
                    <option key={language.id} value={language.id}>{language.displayName}</option>
                ))}

            </select>
        </div>
    );
}

