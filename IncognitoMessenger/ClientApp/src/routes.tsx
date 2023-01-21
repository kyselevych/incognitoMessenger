import AuthorizedRoute from "hoc/AuthorizedRoute";
import UnauthorizedRoute from "hoc/UnauthorizedRoute";
import { createBrowserRouter } from "react-router-dom";
import Auth from "./pages/Auth";

export enum Route {
  Index = '/',
  Auth = '/auth/'
};

export const router = createBrowserRouter([
  {
    path: "/",
    element: <AuthorizedRoute><h1>Main</h1></AuthorizedRoute>
  },
  {
    path: "auth",
    element: <UnauthorizedRoute><Auth /></UnauthorizedRoute>
  },
]);