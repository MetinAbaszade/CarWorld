import { useFormik } from 'formik';
import { AuthService } from '../../../Services';
import { useNavigate } from "react-router-dom";
import * as Yup from 'yup';

export default function useLoginFormValidation() {
    const navigate = useNavigate();

    const validationSchema = Yup.object().shape({
        userName: Yup.string().required('Username is required'),
        password: Yup.string()
            .required('Password is required')
    });

    const formik = useFormik({
        initialValues: {
            userName: 'MatinAbaszade',
            password: 'aaaaaa'
        },
        validationSchema: validationSchema,
        onSubmit: async (values) => {
            let userName = values.userName;
            let password = values.password;
            let loginResponse = await AuthService.Login(userName, password);
            if (!loginResponse.isSuccessfull) {
                const errorModel = loginResponse.errors;
                formik.setErrors(errorModel);
                return;
            }
            navigate("/");
        },
    });

    return { formik };
}
