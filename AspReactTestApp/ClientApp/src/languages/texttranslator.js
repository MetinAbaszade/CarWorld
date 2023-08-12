import { useSelector } from "react-redux";



export default function TextTranslator(tid) {
    const { dictionary } = useSelector((state) => state.languagereducer);
    return dictionary[tid] || tid;
};