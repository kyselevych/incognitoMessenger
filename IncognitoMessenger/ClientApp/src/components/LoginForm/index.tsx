import type { Values } from "behavior/components/LoginForm/types";
import { Button } from "@chakra-ui/react";
import { Form, Formik, FormikHelpers } from "formik";
import validationSchema from "behavior/components/RegistrationForm/validationSchema";
import ChakraInputField from "../ChakraInputField";

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
      validationSchema={validationSchema}
      onSubmit={onSubmit}
    >
      {(props) => (
        <Form>
          <ChakraInputField name="login" label="Login" />
          <ChakraInputField name="password" label="Password" mt={3} />
          <Button isLoading={props.isSubmitting} type='submit' mt={3}>Login</Button>
        </Form>
      )}
    </Formik>
  );
};

export default LoginForm;