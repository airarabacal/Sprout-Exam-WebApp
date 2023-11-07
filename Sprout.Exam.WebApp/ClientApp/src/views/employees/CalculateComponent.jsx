import React, { useState, useEffect } from "react";
import { employeeApiHooks } from '../../store/employeeApiSlice';

const CalculateComponent = (props) => {
    const [ absentDays, setAbsentDays] = useState(0.0);
    const [ workDays, setWorkDays] = useState(0.0);
    const { data: employeeData, isLoading } = employeeApiHooks.useGetEmployeeByIdQuery(props.match.params.id);
    const [ calculateSalary, {isLoading: isLoadingCalculate, data: computedSalary} ] = employeeApiHooks.useCalculateSalaryMutation();

    const formatToCurrency = (amount) => {
        if(isNaN(amount)) amount = 0;
        return new Intl.NumberFormat('ph-PH', { style: 'currency', currency: 'PHP' }).format(amount);
      };

    const handleSubmit = () => {
        calculateSalary({Id: employeeData.id, EmployeeTypeId: employeeData.employeeTypeId, AbsentDays: absentDays, WorkDays : workDays});
        //saveEmployee({Id: 28, FullName: 'Jane Doe test', Birthdate: '2022-06-09', Tin: '123', TypeId: 1});
    }
      const  renderContent = () => {
        return (
            isLoading
                ? <p><em>Loading...</em></p>
                : <div>
                    <form>
                    <div className='form-row'>
                    <div className='form-group col-md-12'>
                    <label>Full Name: <b>{employeeData?.fullName ?? ""}</b></label>
                    </div>

                    </div>

                    <div className='form-row'>
                    <div className='form-group col-md-12'>
                    <label >Birthdate: <b>{employeeData?.birthdate ?? ""}</b></label>
                    </div>
                    </div>

                    <div className="form-row">
                    <div className='form-group col-md-12'>
                    <label>TIN: <b>{employeeData?.tin ?? ""}</b></label>
                    </div>
                    </div>

                    <div className="form-row">
                    <div className='form-group col-md-12'>
                    <label>Employee Type: <b>{employeeData?.EmployeeTypeId === 1?"Regular": "Contractual"}</b></label>
                    </div>
                    </div>

                    { employeeData?.employeeTypeId === 1?
                    <div className="form-row">
                        <div className='form-group col-md-12'><label>Salary: 20000 </label></div>
                        <div className='form-group col-md-12'><label>Tax: 12% </label></div>
                    </div> : <div className="form-row">
                    <div className='form-group col-md-12'><label>Rate Per Day: 500 </label></div>
                    </div> }

                    <div className="form-row">

                    { employeeData?.employeeTypeId === 1? 
                    <div className='form-group col-md-6'>
                    <label htmlFor='inputAbsentDays4'>Absent Days: </label>
                    <input type='number' className='form-control' id='inputAbsentDays4' onChange={(event) => setAbsentDays(Number(event.target.value))} value={absentDays} name="absentDays" placeholder='Absent Days' />
                    </div> :
                    <div className='form-group col-md-6'>
                    <label htmlFor='inputWorkDays4'>Worked Days: </label>
                    <input type='number' className='form-control' id='inputWorkDays4' onChange={(event) => setWorkDays(Number(event.target.value))} value={workDays} name="workedDays" placeholder='Worked Days' />
                    </div>
                    }
                    </div>

                    <div className="form-row">
                    <div className='form-group col-md-12'>
                    <label>Net Income: <b>{formatToCurrency(computedSalary)}</b></label>
                    </div>
                    </div>

                    <button type="submit" onClick={handleSubmit} disabled={isLoadingCalculate} className="btn btn-primary mr-2">{isLoadingCalculate?"Loading...": "Calculate"}</button>
                    <button type="button" onClick={() => props.history.push("/employees/index")} className="btn btn-primary">Back</button>
                    </form>
                    </div>
            
        );
      }
    
        return (
            <div>
                <h1 id="tabelLabel" >Employee Calculate Salary</h1>
                <br/>
                {renderContent()}
          </div>
        );
    };
    
    export default CalculateComponent;