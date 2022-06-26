import React, { useState, useEffect } from "react";
import { employeeApiHooks } from '../../store/employeeApiSlice';
import RequiredFieldDialog from "../../components/OtherComponents/RequiredFieldDialog";

const EditComponent = (props) => {
    const [employee, setEmployee] = useState({ id: 0,fullName: '',birthdate: '',tin: '',typeId: 1 });
    const { data: employeeData, isLoading } = employeeApiHooks.useGetEmployeeByIdQuery(props.match.params.id);
    const [ updateEmployee, {isSuccess: isSuccessUpdate}] = employeeApiHooks.useUpdateEmployeeMutation();
    const [openRequiredFieldDialog, setOpenRequiredFieldDialog] = useState(false);
    const [requiredField, setRequiredField] = useState("")

  useEffect(() => {
    if(isLoading) return;

    setEmployee(employeeData)
  }, [isLoading, employeeData])

  useEffect(() =>{
    if(isSuccessUpdate){
        alert("Employee successfully saved");
        props.history.push("/employees/index");
    }
  }, [isSuccessUpdate, props.history])

  const handleSubmit = (e) => {
    e.preventDefault();
    if(checkRequriedField())
      updateEmployee({Id: employee.id, FullName: employee.fullName, Birthdate: employee.birthdate, Tin: employee.tin, TypeId: employee.typeId});
  }

  const checkRequriedField = () => {
    if(employee.fullName === ""){
      setRequiredField("Full Name");
      setOpenRequiredFieldDialog(true);
      return false;
    }
    else if(employee.birthdate === ""){
      setRequiredField("Birthdate");
      setOpenRequiredFieldDialog(true);
      return false;
    }
    else if(employee.tin === ""){
      setRequiredField("TIN");
      setOpenRequiredFieldDialog(true);
      return false;
    }
    else if(employee.typeId === ""){
      setRequiredField("Employee Type");
      setOpenRequiredFieldDialog(true);
      return false;
    }
    return true;
  }

  const handleChangeName = (event) => {
    setEmployee({ ...employee, fullName: event.target.value});
  }

  const handleChangeBirthDate = (event) => {
    setEmployee({ ...employee, birthdate: event.target.value});
  }

  const handleChangeTIN = (event) => {
    setEmployee({ ...employee, tin: event.target.value});
  }

  const handleChangeTypeId = (event) => {
    setEmployee({ ...employee, typeId: event.target.value});
  }

  const  renderContent = () => {
    return (
        // isLoading
        // ? <p><em>Loading...</em></p>
        // : 
        <div>
            <form>
            <div className='form-row'>
            <div className='form-group col-md-6'>
            <label htmlFor='inputFullName4'>Full Name: *</label>
            <input type='text' className='form-control' id='inputFullName4' onChange={handleChangeName} name="fullName" value={employee?.fullName ?? ""} placeholder='Full Name' />
            </div>
            <div className='form-group col-md-6'>
            <label htmlFor='inputBirthdate4'>Birthdate: *</label>
            <input type='date' className='form-control' id='inputBirthdate4' onChange={handleChangeBirthDate} name="birthdate" value={employee?.birthdate ?? ""} placeholder='Birthdate' />
            </div>
            </div>
            <div className="form-row">
            <div className='form-group col-md-6'>
            <label htmlFor='inputTin4'>TIN: *</label>
            <input type='text' className='form-control' id='inputTin4' onChange={handleChangeTIN} value={employee?.tin ?? ""} name="tin" placeholder='TIN' />
            </div>
            <div className='form-group col-md-6'>
            <label htmlFor='inputEmployeeType4'>Employee Type: *</label>
            <select id='inputEmployeeType4' onChange={handleChangeTypeId} value={employee?.typeId ?? ""}  name="typeId" className='form-control'>
                <option value='1'>Regular</option>
                <option value='2'>Contractual</option>
            </select>
            </div>
            </div>
            <button type="submit" onClick={handleSubmit.bind(this)} className="btn btn-primary mr-2">{"Save"}</button>
            <button type="button" onClick={() => props.history.push("/employees/index")} className="btn btn-primary">Back</button>
            </form>
        </div>
        
    );
  }

  return (
    <div>
    <h1 id="tabelLabel" >Employee Edit</h1>
    <p>All fields are required</p>
    {renderContent()}

    <RequiredFieldDialog RequiredField={requiredField} open={openRequiredFieldDialog} handleClose={() => setOpenRequiredFieldDialog(false)} />
  </div>
);
};

export default EditComponent;