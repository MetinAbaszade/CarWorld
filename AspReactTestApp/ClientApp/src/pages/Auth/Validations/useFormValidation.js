import { useFormik } from 'formik';
import { AuthService } from '../../../Services';
import { useNavigate } from "react-router-dom";
import * as Yup from 'yup';

export default function useFormValidation(selectedImage) {
    const navigate = useNavigate();

    const formik = useFormik({
        initialValues: {
            email: 'metin.abaszade@gmail.com',
            name: 'Matin',
            surname: 'Abaszade',
            userName: 'MatinAbaszade',
            password: 'aaaaaa',
            retypePassword: 'aaaaaa',
        },
        onSubmit: async (values) => {
            let name = values.name
            let surname = values.surname;
            let username = values.userName
            let email = values.userName
            let password = values.password;
            let retypePassword = values.retypePassword;

            let authResponse = await AuthService.Register(name, surname, username, email, selectedImage, password, retypePassword);
            if (authResponse.isSuccessfull)
                navigate("/Auth/Login");
        },
    });

    //#region Schemas to validate each page:
    
    const FirstPageSchema = Yup.object().shape({
        name: Yup.string().required('Name is required'),
        surname: Yup.string().required('Surname is required'),
        userName: Yup.string().required('Username is required'),
        password: Yup.string()
            .min(6, 'Password must be at least 6 characters')
            .required('Password is required'),
        retypePassword: Yup.string()
            .oneOf([Yup.ref('password'), null], 'Passwords must match')
            .required('Retype password is required'),
    });

    const SecondPageSchema = Yup.object().shape({
        email: Yup.string().email("Invalid email").required("Email is required"),
    });

    //#endregion

    const validateForm = async (schema, valuesToValidate) => {
        try {
            await schema.validate(valuesToValidate, { abortEarly: false });
            formik.setErrors({});
            return true;
        } catch (err) {
            let errorMessages = err.inner.reduce((errors, error) => {
                return { ...errors, [error.path]: error.message };
            }, {});

            formik.setErrors(errorMessages);
            return false;
        }
    }

    return { formik, FirstPageSchema, SecondPageSchema, validateForm };
}
