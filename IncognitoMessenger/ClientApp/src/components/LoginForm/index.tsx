import type { Values } from "components/LoginForm/types";
import { Button } from "@chakra-ui/react";
import { Form, Formik, FormikHelpers } from "formik";
import ChakraInputField from "../ChakraInputField";
import validationSchema from "./validationSchema";

type Props = {
  onSubmit: ((values: Values, formikHelpers: FormikHelpers<Values>) => void | Promise<any>) | (() => void)
};

const LoginForm = ({onSubmit}: Props) => {
  const initialValues: Values = {
    login: '',
    password: ''
  };

  return (
    <Formik
      initialValues={initialValues}
      onSubmit={onSubmit}
      validationSchema={validationSchema}
    >
      {props => (
        <Form onSubmit={props.handleSubmit}>
          <ChakraInputField name="login" label="Login" isValidate />
          <ChakraInputField name="password" label="Password" mt={3} type="password" isValidate/>
          <Button isLoading={props.isSubmitting} type='submit' mt={3}>Login</Button>
        </Form>
      )}
    </Formik>
  );
};

export default LoginForm;