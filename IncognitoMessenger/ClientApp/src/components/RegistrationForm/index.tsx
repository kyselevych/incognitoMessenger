import type { Values } from "behavior/components/RegistrationForm/types";
import { Button } from "@chakra-ui/react";
import { Form, Formik, FormikHelpers } from "formik";
import validationSchema from "behavior/components/RegistrationForm/validationSchema";
import ChakraInputField from "../ChakraInputField";

type Props = {
  onSubmit: ((values: Values, formikHelpers: FormikHelpers<Values>) => void | Promise<any>) | (() => void)
};

const RegistrationForm = ({onSubmit}: Props) => {
  const initialValues: Values = {
    login: '',
    password: '',
    repeatPassword: '',
    nickname: ''
  };

  return (
    <Formik
      initialValues={initialValues}
      validationSchema={validationSchema}
      onSubmit={onSubmit}
    >
      {(props) => (
        <Form>
          <ChakraInputField name="login" label="Login" isValidate/>
          <ChakraInputField name="password" label="Password" mt={3} isValidate/>
          <ChakraInputField name="repeatPassword" label="Repeat password" mt={3} isValidate/>
          <ChakraInputField name="nickname" label="Nickname" mt={3} isValidate/>
          <Button isLoading={props.isSubmitting} type='submit' mt={3}>Login</Button>
        </Form>
      )}
    </Formik>
  );
};

export default RegistrationForm;