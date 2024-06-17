//Any Select Contains 
using System.IO;
using System.Reflection;

var traderTerminalTimeslotVehicleReservations = (from a in terminalTimeslotVehicleReservations
                                                 where a.Members.Any(x => x.MemberType is MemberType.CargoOwner && x.MemberId == currentMember.MemberId)
                                                       || a.Members.Any(x => x.MemberInformation.Select(x => x.CargoOwnerId).Contains(currentMember.MemberId))
                                                 select a).ToList();


//LoadAsync
restrictionAfterUpdate.DateUpdated = DateTimeHelper.UtcNow();

var entityFromDb = await context.TerminalRestrictions
    .Include(x => x.TerminalRestrictionReservation).ThenInclude(x => x.TerminalTimeslotVehicleReservations).ThenInclude(x => x.Members)
    .Include(x => x.TerminalTimeslotVehicleToRestrictions).ThenInclude(x => x.TerminalTimeslotVehicle).ThenInclude(x => x.Members)
    .Where(x => x.Id == restrictionAfterUpdate.Id).FirstOrDefaultAsync();

var entityFromDb = await context.TerminalRestrictions.FirstOrDefaultAsync(x => x.Id == restrictionAfterUpdate.Id);

if (!restriction.Shared && entityFromDb.Area?.AreaId != restriction.Area?.AreaId)
{
    entityFromDb.Area = restriction.Area;
}

await context.Entry(entityFromDb).Reference(x => x.TerminalRestrictionReservation).LoadAsync();
await context.Entry(entityFromDb).Collection(x => x.TerminalTimeslotVehicleToRestrictions).LoadAsync();
await context.Entry(entityFromDb.TerminalRestrictionReservation).Collection(x => x.TerminalTimeslotVehicleReservations).LoadAsync();
foreach (var item in entityFromDb.TerminalRestrictionReservation.TerminalTimeslotVehicleReservations)
{
    await context.Entry(item).Collection(x => x.Members).LoadAsync();
}


var entityFromDbCrops = entityFromDb.Crops.ToList();

var excludeRestrictionCropsFromRequest = restriction.Crops.Select(x => x.CropId).Except(entityFromDb.Crops.Select(x => x.CropId)).ToList();

//Select Except
&& !(x.Crops.Select(x => x.CropId).Except(cropIdsEntityFromDb)).Any()

//Collection with min values (window function)
var members = drivers.Select(x => new
{
    x.TerminalId,
    x.MemberId,
    UserId = driversInfo.SingleOrDefault(d => d.Id == x.MemberId)?.User?.Id ?? 0
}).GroupBy(x => new
{
    x.TerminalId,
    x.UserId
}).Select(group => new
{
    group.Key.TerminalId,
    group.Key.UserId,
    MemberId = group.Min(x => x.MemberId)
}).ToList();

//JoinAndSplit String
string.Join(',', a.RestrictionOwners.Select(x => x.MemberId))
RestrictionOwnerIds = grouped.Key.Split(',').Select(x => Convert.ToInt32(x)).ToList()

//Join Tables And Query
                                return await (from v in context.TerminalTimeslotVehicles
                    .Include(x => x.Events)
                    .Include(x => x.EventTimeslotVehicles)
                    .Include(x => x.Calls)
                    .Include(x => x.TerminalTimeslot).ThenInclude(x => x.TerminalSetting)
                    .Include(x => x.TerminalTimeslotVehicleToRestrictions)
                    .ThenInclude(x => x.TerminalRestriction)
                    .ThenInclude(x => x.Crops)
                    .ThenInclude(x => x.CropWithParameterCombinationToTerminalRestrictionGroupCrops)
                    .ThenInclude(x => x.TerminalAdditionalParameterCombination)
                    .ThenInclude(x => x.ParameterToTerminalAdditionalParameterCombinations)
                    .ThenInclude(x => x.TerminalAdditionalParameter)
                                              join tt in context.TerminalTimeslots on v.TerminalTimeslotId equals tt.Id
                                              join ts in context.TerminalSettings on tt.TerminalSettingId equals ts.Id
                                              where ts.TerminalId == terminalId && v.Deleted == null &&
                                                    tt.Date.AddMinutes(ts.TimeslotDuration * tt.TimeslotNumber) >= DateTimeHelper.Now().AddDays(-1) &&
                                                    tt.Date.AddMinutes(ts.TimeslotDuration * tt.TimeslotNumber) <= DateTimeHelper.Now().AddMinutes(forecastMinutes)
                                              select v).ToListAsync(token);



foreach (var timeslotVehicle in terminalVehicles)
{
    await _context.Entry(timeslotVehicle).Reference(x => x.TerminalTimeslot).LoadAsync();
    await _context.Entry(timeslotVehicle).Collection(x => x.Events).LoadAsync();
    await _context.Entry(timeslotVehicle).Collection(x => x.EventTimeslotVehicles).LoadAsync();
    await _context.Entry(timeslotVehicle).Collection(x => x.Calls).LoadAsync();
}

var timeslotVehicleWithStatuses = _terminalTimeslotVehicleStatusService
    .GetTerminalTimeslotVehicleDictionaryWithStatuses(terminalVehicles, setting);

foreach (var terminalVehicle in terminalVehicles)
{
    await _context.Entry(terminalVehicle)
        .Collection(x => x.TerminalTimeslotVehicleToRestrictions)
        .Query()
        .Include(x => x.TerminalRestriction)
        .ThenInclude(x => x.Crops)
        .ThenInclude(x => x.CropWithParameterCombinationToTerminalRestrictionGroupCrops)
        .ThenInclude(x => x.TerminalAdditionalParameterCombination)
        .ThenInclude(x => x.ParameterToTerminalAdditionalParameterCombinations)
        .ThenInclude(x => x.TerminalAdditionalParameter)
        .LoadAsync();
}

//Join Tables And Let

return (from a in terminalTimeslotVehicles
let outUnloadingEvent = a.EventVehicles
	.Where(x => x.AreaType == AreaType.Unloading
	            && x.EventType == EventType.VehicleOutArea).FirstOrDefault()
let unloadedEvent = a.EventTimeslotVehicles
	.Where(x => x.Parameters.WeightUnloaded > 0
	            && x.EventType == EventType.DriverLoadingCompleted)
where !unloadedEvent.Any() && outUnloadingEvent != null

//Action
DateTimeHelper.OverrideNow(() => DateTime.Today.AddHours(10).AddMinutes(DateTime.Now.Minute).AddSeconds(DateTime.Now.Second));
DateTimeHelper.OverrideNowUtc(() => DateTime.Today.AddHours(7).AddMinutes(DateTime.Now.Minute).AddSeconds(DateTime.Now.Second));

public static class DateTimeHelper
{
    private static Func<DateTime> GetNow = () => DateTime.Now;
    private static Func<DateTime> GetNowUtc = () => DateTime.UtcNow;

    public static DateTime Now()
    {
        return GetNow.Invoke();
    }

    public static DateTime UtcNow()
    {
        return GetNowUtc.Invoke();
    }

    public static void OverrideNow(Func<DateTime> newNow)
    {
        GetNow = newNow;
    }
    public static void OverrideNowUtc(Func<DateTime> newNow)
    {
        GetNowUtc = newNow;
    }
}