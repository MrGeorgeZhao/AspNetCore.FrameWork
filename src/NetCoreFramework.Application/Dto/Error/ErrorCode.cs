using System;
using System.Collections.Generic;
using System.Text;

namespace NetCoreFramework.Application.Dto.Error
{
    public enum ErrorCode
    {
        [ErrorCode(Message = "正确")]
        NoError = 0,

        [ErrorCode(Message = "用户不存在")]
        UserNotExisit = 1000,
        [ErrorCode(Message = "用户名或密码错误")]
        UserOrPassWrong = 1001,
        [ErrorCode(Message = "原密码有误，请重新输入！")]
        OldPassWrong = 1002,
        [ErrorCode(Message = "用户名或密码不能为空")]
        UserOrPassEmpty = 1003,
        [ErrorCode(Message = "用户名不能为空")]
        UserNameEmpty = 1004,
        [ErrorCode(Message = "手机号已存在")]
        MobileRepeat = 1005,
        [ErrorCode(Message = "手机号格式不正确")]
        MobileFormatError = 1006,


        [ErrorCode(Message = "文件不能为空")]
        FileNull = 1100,


        [ErrorCode(Message = "学科名称不能为空")]
        CourseNameEmpty = 2001,
        [ErrorCode(Message = "学科已存在")]
        CourseIsExist = 2002,
        [ErrorCode(Message = "新建学科失败")]
        CourseCreateFail = 2003,
        [ErrorCode(Message = "编辑学科失败")]
        CourseNameEditFail = 2004,
        [ErrorCode(Message = "调整学科顺序失败")]
        CourseOrderEditFail = 2005,
        [ErrorCode(Message = "删除学科失败")]
        CourseDeleteFail = 2006,
        [ErrorCode(Message = "学科不存在")]
        CourseNotExist = 2007,

        [ErrorCode(Message = "当前学年学期不存在")]
        YearTermNotExist = 2010,
        [ErrorCode(Message = "本学期未结束，不允许创建新学期，请检查本学期时间范围设置")]
        YearTermNotEnd = 2011,
        [ErrorCode(Message = "学年学期创建失败")]
        YearTermCreateFail = 2012,
        [ErrorCode(Message = "学年学期编辑失败")]
        YearTermEditFail = 2013,
        [ErrorCode(Message = "ID不能为空")]
        IdCanNotBeNull = 2014,
        [ErrorCode(Message = "名称不能为空")]
        YearTermNameCanNotBeNull = 2015,
        [ErrorCode(Message = "开始日期不能为空")]
        YearTermStartCanNotBeNull = 2016,
        [ErrorCode(Message = "结束日期不能为空")]
        YearTermEndCanNotBeNull = 2017,
 

        [ErrorCode(Message = "年级学期不能为空")]
        GradeTermIsNull = 2020,

        [ErrorCode(Message = "课本名称不能为空")]
        SubjectNameIsNull = 2026,
        [ErrorCode(Message = "新建课本失败")]
        SubjectCreateFail = 2027,
        [ErrorCode(Message = "编辑课本失败")]
        SubjectEditFail = 2028,
        [ErrorCode(Message = "删除课本失败")]
        SubjectDelFail = 2029,
        [ErrorCode(Message = "课本ID不能为空")]
        SubjectIdIsNull = 2030,
        [ErrorCode(Message = "课本中已存在该课题")]
        SubjectTitleExist = 2031,

        [ErrorCode(Message = "新建课题失败")]
        SubjectTitleAddFail = 2032,
        [ErrorCode(Message = "编辑课题失败")]
        SubjectTitleEditFail = 2033,
        [ErrorCode(Message = "删除课题失败")]
        SubjectTitleDelFail = 2034,
        [ErrorCode(Message = "删除课本失败,课本下有课题，请先删除")]
        SubjectDelFailHasTitle = 2035,

        [ErrorCode(Message = "新增年级学科失败")]
        GradeCourseAddFail = 2040,
        [ErrorCode(Message = "一级学科不存在")]
        FirstCourseNotExist = 2041,
        [ErrorCode(Message = "年级不存在")]
        GradeNotExist = 2042,
        [ErrorCode(Message = "二级学科名称不能为空")]
        SecondCourseNameIsEmpty = 2043,

        [ErrorCode(Message = "编辑年级学科失败")]
        GradeCourseEditFail = 2044,
        [ErrorCode(Message = "该年级已经存在此学科")]
        GradeCourseExist = 2045,
        [ErrorCode(Message = "删除年级学科失败")]
        GradeCourseDelFail = 2046,

        [ErrorCode(Message = "编辑教师任课信息失败")]
        TeacherClassCourseEditFail = 2050,
        [ErrorCode(Message = "编辑课程周课时失败")]
        ClassCourseWeeklyHourEditFail = 2051,
        [ErrorCode(Message = "导入Excel失败")]
        ImportExcelFail = 2052,

    }
}
