import { Route, Routes } from "react-router-dom";
import Home from "../pages/Home/Home";
import Contact from "../pages/Contact";
import About from "../pages/About";
import Profile from "../pages/Profile";
import Page404 from "../pages/Page404";
import Login from "../pages/Auth/Login/Login";
import Register from "../pages/Auth/Register/Register";
import NavMenu from "./NavMenu/NavMenu";
import CarDetails from "../pages/CarDetails/CarDetails";
import AuthorizedRoute from "./AuthorizedRoute";

const AppRoutes = () => {
    return (
        <Routes>
            <Route path="/" element={<AuthorizedRoute><NavMenu /><Home /></AuthorizedRoute>} />
            <Route path="/contact" element={<Contact />} />
            <Route path="/about" element={<About />} />
            <Route path="/profile" element={<AuthorizedRoute><NavMenu /><Profile /></AuthorizedRoute>} />
            <Route path="/car/:id" element={<AuthorizedRoute><NavMenu /><CarDetails /></AuthorizedRoute>} />
            <Route path="/auth">
                <Route path="login" element={<Login />} />
                <Route path="register" element={<Register />} />
            </Route>

            <Route path="*" element={<Page404 />} />
        </Routes>
    )
};

export default AppRoutes;
