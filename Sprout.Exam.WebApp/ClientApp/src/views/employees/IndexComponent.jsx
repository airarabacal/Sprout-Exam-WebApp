import React, { useState, useEffect } from "react";
import { employeeApiHooks } from '../../store/employeeApiSlice';

const IndexComponent = (props) => {
const { data: employeeData, isLoading } = employeeApiHooks.useGetEmployeesQuery();
const [ deleteEmployee, {isSuccess: isSuccessDelete}] = employeeApiHooks.useDeleteEmployeeMutation();
  const  renderEmployeesTable = () => {
    return (
      <table className='table table-striped' aria-labelledby="tabelLabel">
        <thead>
          <tr>
            <th>Full Name</th>
            <th>Birthdate</th>
            <th>TIN</th>
            <th>Type</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
          {employeeData?.map(employee =>
            <tr key={employee.id}>
              <td>{employee.fullName}</td>
              <td>{employee.birthdate}</td>
              <td>{employee.tin}</td>
              <td>{employee.typeId === 1?"Regular":"Contractual"}</td>
              <td>
              <button type='button' className='btn btn-info mr-2' onClick={() => props.history.push("/employees/" + employee.id + "/edit")} >Edit</button>
              <button type='button' className='btn btn-primary mr-2' onClick={() => props.history.push("/employees/" + employee.id + "/calculate")}>Calculate</button>
            <button type='button' className='btn btn-danger mr-2' onClick={() => {
              if (window.confirm("Are you sure you want to delete?")) {
                deleteEmployee(employee.id)
              } 
            } }>Delete</button></td>
            </tr>
          )}
        </tbody>
      </table>
    );
  }

  return (
    <div>
      <h1 id="tabelLabel" >Employees</h1>
      <p>This page should fetch data from the server.</p>
      <p><button type='button' className='btn btn-success mr-2' onClick={() => props.history.push("/employees/create")} >Create</button></p>
      {isLoading
      ? <p><em>Loading...</em></p>
      : renderEmployeesTable()}
    </div>
  );
};

export default IndexComponent;