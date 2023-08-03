import { Route, Routes } from "react-router-dom";
import Home from "../pages/Home/Home";
import Contact from "../pages/Contact";
import About from "../pages/About";
import Profile from "../pages/Profile";
import Page404 from "../pages/Page404";
import Post from "../pages/posts/Post";
import PostsLayout from "../pages/posts";
import Posts from "../pages/posts/Posts";
import PostNotFound from "../pages/posts/PostNotFound";
import Login from "../pages/Auth/Login/Login";
import Register from "../pages/Auth/Register/Register";
import NavMenu from "./NavMenu";
import CarDetails from "../pages/CarDetails";
import AuthorizedRoute from "./AuthorizedRoute";

const AppRoutes = () => {
    return (
        <Routes>
            <Route path="/" element={<AuthorizedRoute><NavMenu /><Home /></AuthorizedRoute>} />
            <Route path="/contact" element={<Contact />} />
            <Route path="/about" element={<About />} />
            <Route path="/profile" element={<AuthorizedRoute><NavMenu /><Profile /></AuthorizedRoute>} />
            <Route path="/car/:id" element={<AuthorizedRoute><CarDetails /></AuthorizedRoute>} />
            <Route path="/posts" element={<><NavMenu /><PostsLayout /></>}>
                <Route index={true} element={<Posts />} />
                <Route path="/posts/:url/:id" element={<Post />} />
                <Route path="*" element={<PostNotFound />} />
            </Route>

            <Route path="/auth">
                <Route path="login" element={<Login />} />
                <Route path="register" element={<Register />} />
            </Route>

            <Route path="*" element={<Page404 />} />
        </Routes>
    )
};

export default AppRoutes;
