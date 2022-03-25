import { Route, Routes } from "react-router-dom";
import { AboutPage } from "../pages/basic/about";
import { HomePage } from "../pages/basic/home";


export function PageRoute(){

    return (
        <Routes>
            <Route path="/" element={<HomePage />} />
            <Route path="about" element={<AboutPage />} />
        </Routes>
    );
}