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
            verificationCode: new Array(6).fill('')
        },
        onSubmit: async (values) => {
            let name = values.name
            let surname = values.surname;
            let userName = values.userName
            let email = values.email
            let password = values.password;
            let retypePassword = values.retypePassword;
            let verificationCode = values.verificationCode;

            var verificationCodeString = verificationCode.join('');
            var emailVerificationResponse = await AuthService.CheckVerificationCode(email, verificationCodeString)
            
            if (!emailVerificationResponse.isSuccessfull) {
                const errorModel = emailVerificationResponse.errors;
                formik.setErrors(errorModel);
                return;
            }
         
            let registerResponse = await AuthService.Register(name, surname, userName, email, selectedImage, password, retypePassword);

            if (!registerResponse.isSuccessfull) {
                const registerResponseJson = await registerResponse.json();
                const errorModel = registerResponseJson.errors;
                formik.setErrors(errorModel);
                return;
            }
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
